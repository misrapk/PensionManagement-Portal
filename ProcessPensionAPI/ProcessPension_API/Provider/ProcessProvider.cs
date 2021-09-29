using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProcessPension_API.Models;

namespace ProcessPension_API.Provider
{
	public class ProcessProvider : IProcessProvider
	{
		public HttpResponseMessage PensionDetail(string aadhar)
		{
			ProcessProvider banktype = new();

			HttpResponseMessage res = new();

			//link from PensionerDetailAPI
			//string uriConn = "https://localhost:5001/";
			string uriConn = "https://pensionerdetailapiget.azurewebsites.net/";

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(uriConn);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
				try
				{
					res = client.GetAsync("api/PensionerDetail/" + aadhar).Result;
				}
				catch (Exception e)
				{
					//_log4net.Error("Exception Occured" + e);
					return null;
				}
			}
			return res;
		}

		public PensionDetail GetClientInfo(string aadhar)
		{
			PensionDetail result = new();
			HttpResponseMessage res = PensionDetail(aadhar);
			if (res == null)
			{
				result = null;
				return null;
			}
			string responseValue = res.Content.ReadAsStringAsync().Result;
			result = JsonConvert.DeserializeObject<PensionDetail>(responseValue);
			if (result == null)
			{
				return null;
			}
			return result;
		}


		public ValueforCalculation GetCalculationValues(string aadhar)
		{
			PensionerDetail result = new();
			HttpResponseMessage res = PensionDetail(aadhar);
			if (res == null)
			{
				result = null;
				return null;
			}
			string responseValue = res.Content.ReadAsStringAsync().Result;
			result = JsonConvert.DeserializeObject<PensionerDetail>(responseValue);

			ValueforCalculation Values = new()
			{
				SalaryEarned = result.SalaryEarned,
				Allowances = result.Allowances,
				BankType = (int)result.BankType,
				PensionType = (PensionType)result.PensionType
			};
			return Values;
		}

		public HttpResponseMessage GetDisbursementMessage(ProcessPensionInput processInput)
		{
			//link from disbursement
			string uriConn2 = "https://pensiondisbursementapi.azurewebsites.net/";
			//string uriConn2 = "https://localhost:44369/";
			HttpResponseMessage res = new();

			using (var client1 = new HttpClient())
			{
				client1.BaseAddress = new Uri(uriConn2);

				StringContent content = new(JsonConvert.SerializeObject(processInput), Encoding.UTF8, "application/json");

				client1.DefaultRequestHeaders.Clear();

				client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				try
				{
					res = client1.PostAsync("api/Disbursement", content).Result;
				}
				catch (Exception e)
				{
					//_log4net.Error("Exception Occured" + e);
					res = null;
				}
			}

			return res;
		}

		//method to calculate pension amount
		public double CalculatePensionAmount(int salary, int allowances, int bankType, PensionType pensionType)
		{

			double pensionAmount=0;
			if (pensionType == PensionType.Self)
				pensionAmount = (0.8 * salary) + allowances;
			else
				pensionAmount = (0.5 * salary) + allowances;

			if (bankType == 1)
				pensionAmount += 500;
			else
				pensionAmount += 550;

			return pensionAmount;
		}

		
	}
}
