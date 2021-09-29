using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessPension_API.Models;
using ProcessPension_API.Provider;
using ProcessPension_API.Repository;

namespace ProcessPension_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProcessPensionController : ControllerBase
	{
		private IProcessRepo _repo;
		private IProcessProvider _provider;
		private ProcessResponse processResponse;

		//dependecy Injection
		public ProcessPensionController(IProcessRepo repo)
		{
			_repo = repo;

		}

		/*take input from mvc app
		 * call the pension Detail api and check all the values
		 * call pensionDisbursement to get status code
		 * return --> details to be displayed
		 */

		[Route("[action]")]
		[HttpPost]

        public IActionResult ProcessPension(PensionerInput processPensionInput)
        {
			//_log4net.Info("Pensioner details invoked from Client Input");
			PensionerInput client = new()
			{
				Name = processPensionInput.Name,
				AadharNumber = processPensionInput.AadharNumber,
				PAN = processPensionInput.PAN,
				DateOfBirth = processPensionInput.DateOfBirth,
				PensionType = processPensionInput.PensionType
			};

			//repo = new ProcessRepo(pro);
			PensionDetail pensionDetail = _repo.GetClientInfo(client.AadharNumber);

            if (pensionDetail == null)
            {
                PensionDetail mvc = new();
                mvc.Name = "";
                mvc.Pan = "";
                mvc.PensionAmount = 0;
                mvc.DateOfBirth = new DateTime(2000, 01, 01);
                mvc.BankType = 1;
                mvc.AadharNumber = "***";
                mvc.Status = 0;

                processResponse = new ProcessResponse()
                {
                    Status = 0,
                    PensionAmount = mvc.PensionAmount
                };

                return BadRequest(processResponse);
            }



            double pensionAmount;

            ValueforCalculation pensionerInfo = _repo.GetCalculationValues(client.AadharNumber);
            pensionAmount = CalculatePensionAmount(pensionerInfo.SalaryEarned, pensionerInfo.Allowances, pensionerInfo.BankType, processPensionInput.PensionType);

            int statusCode;

            PensionDetail mvcClientOutput = new();

            if (client.AadharNumber.Equals(pensionDetail.AadharNumber) && client.PAN.Equals(pensionDetail.Pan) && client.Name.Equals(pensionDetail.Name))
            {
                mvcClientOutput.Name = pensionDetail.Name;
                mvcClientOutput.Pan = pensionDetail.Pan;
                mvcClientOutput.PensionAmount = pensionAmount;
                mvcClientOutput.DateOfBirth = pensionDetail.DateOfBirth.Date;
                mvcClientOutput.PensionType = pensionerInfo.PensionType;
                mvcClientOutput.BankType = pensionerInfo.BankType;
                mvcClientOutput.AadharNumber = pensionDetail.AadharNumber;
                mvcClientOutput.Status = 10;
            }
            else
            {
                mvcClientOutput.Name = "";
                mvcClientOutput.Pan = "";
                mvcClientOutput.PensionAmount = 0;
                mvcClientOutput.DateOfBirth = new DateTime(2000, 01, 01);
                mvcClientOutput.PensionType = pensionerInfo.PensionType;
                mvcClientOutput.BankType = 1;
                mvcClientOutput.AadharNumber = "****";
                mvcClientOutput.Status = 21;

                processResponse = new ProcessResponse()
                {
                    Status = 0,
                    PensionAmount = mvcClientOutput.PensionAmount
                };

                return Ok(processResponse);
            }

			ProcessPensionInput input = new()
			{
				AadharNumber = mvcClientOutput.AadharNumber,
				PensionAmount = mvcClientOutput.PensionAmount
			};
			if (mvcClientOutput.BankType == 1)
            {
                input.BankCharge = 500;
            }
            else
            {
                input.BankCharge = 550;
            }




            HttpResponseMessage response = _repo.GetDisbursementMessage(input);

            if (response != null)
            {
                string status = response.Content.ReadAsStringAsync().Result;
				//statusCode = Int32.Parse(status);
				ProcessPensionResponse res = JsonConvert.DeserializeObject<ProcessPensionResponse>(status);


				statusCode = res.processPensionStatusCode;
				processResponse = new ProcessResponse()
                    {
                        Status = statusCode,
                        PensionAmount = mvcClientOutput.PensionAmount
                    };

                    return Ok(processResponse);
                
		
            }
            return Ok(processResponse);
        }

        private double CalculatePensionAmount(int salaryEarned, int allowances, int bankType, PensionType pensionType)
		{
			double pensionAmount;

			//repo = new ProcessRepo(pro);

			pensionAmount = _repo.CalculatePensionAmount(salaryEarned, allowances, bankType, pensionType);

			return pensionAmount;
		}
	}

	public class ProcessPensionResponse
	{
		public int processPensionStatusCode { get; set; }
	}
}
