using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PensionerDetailAPI.Controllers;
using PensionerDetailAPI.Models;

namespace PensionerDetailAPI.Repository
{
	public class PensionerDetailRepo : IPensionerDetail
	{
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerDetailController));

        public List<PensionerDetail> GetDetailsCSV()
		{
            _log4net.Info("Checking Data in the record");  // Logging Implemented
            List<PensionerDetail> pensionerdetail = new();
            try
            {
				

				// Initializing the csvConn  for the File path
				using StreamReader sr = new("data.csv");
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] values = line.Split(',');
					pensionerdetail.Add(new PensionerDetail()
					{
						Name = values[0],
						DateofBirth = Convert.ToDateTime(values[1]),
						PAN = values[2],
						SalaryEarned = Convert.ToInt32(values[4]),
						Allowances = Convert.ToInt32(values[5]),
						AadharNumber = values[3],
						PensionType = (PensionTypeValue)Enum.Parse(typeof(PensionTypeValue), values[6]),
						BankName = values[7],
						AccountNumber = values[8],
						BankType = (BankType)Enum.Parse(typeof(BankType), values[9])
					});
				}
			}
            catch (NullReferenceException e)
            {
                _log4net.Error("NOt Registered User" + e);
                return null;
            }
            catch (Exception e)
            {
                _log4net.Error("Not Registered User" + e);
                return null;
            }
			return pensionerdetail.ToList();
        }

		public PensionerDetail PensionerDetailByAadhaar(string aadhar)
		{
            List<PensionerDetail> pensionDetails = GetDetailsCSV();
            _log4net.Info("Showing details based on Aadhar number");

            return pensionDetails.FirstOrDefault(s => s.AadharNumber == aadhar);
        }
	}
}
