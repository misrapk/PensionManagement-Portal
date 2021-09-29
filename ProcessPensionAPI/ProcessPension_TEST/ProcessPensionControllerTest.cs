using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProcessPension_API.Controllers;
using ProcessPension_API.Models;
using ProcessPension_API.Repository;

namespace ProcessPension_TEST
{
	class ProcessPensionControllerTest
	{
        private Mock<IProcessRepo> repo;
        private ProcessPensionController processPensionController;
        PensionerInput pensionerInput = new PensionerInput()
        {
            Name = "Diksha",
            PAN = "BCFPN1234F",
            DateOfBirth = new DateTime(2000, 01, 01),
            PensionType = PensionType.Family,
            AadharNumber = "222222223333",
        };
        [Test]
        public void ProcessPension_Called_ReturnPensionDetail()
		{
            ProcessPensionInput input = new()
            {
                AadharNumber = "111122223333",
                BankCharge = 550,
                PensionAmount = 25500
            };
            ValueforCalculation calc = new()
            {
                BankType = 1,
                SalaryEarned = 25000,
                Allowances = 1000,
                PensionType = PensionType.Self
            };
            PensionDetail pensionDetail = new()
            {
                Name = "Ritvik",
                DateOfBirth = new DateTime(1998 - 03 - 01),
                Pan = "EYRMP2435Q",
                AadharNumber = "111122223333",
                PensionType = PensionType.Self,
                PensionAmount = 35000,
                BankType = 1,
                Status = 21
            };

            repo = new Mock<IProcessRepo>();
            processPensionController = new ProcessPensionController(repo.Object);

            repo.Setup(r => r.GetClientInfo(pensionerInput.AadharNumber)).Returns(pensionDetail);
            repo.Setup(r => r.GetCalculationValues(pensionerInput.AadharNumber)).Returns(calc);
            repo.Setup(r => r.GetDisbursementMessage(input)).Returns(new HttpResponseMessage(HttpStatusCode.OK));

            repo.Setup(r => r.CalculatePensionAmount(25000,1000,1, PensionType.Self)).Returns(21500.00);


            var result = processPensionController.ProcessPension(pensionerInput);


            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }


        [Test]
        public void ProcessPension_Called_ReturnBadRequest()
        {
            ProcessPensionInput input = new()
            {
                AadharNumber = "111122223333",
                BankCharge = 550,
                PensionAmount = 25500
            };
            ValueforCalculation calc = new()
            {
                BankType = 1,
                SalaryEarned = 25000,
                Allowances = 1000,
                PensionType = PensionType.Self
            };
            PensionDetail pensionDetail = null;

            repo = new Mock<IProcessRepo>();
            processPensionController = new ProcessPensionController(repo.Object);

            repo.Setup(r => r.GetClientInfo(pensionerInput.AadharNumber)).Returns(pensionDetail);
            repo.Setup(r => r.GetCalculationValues(pensionerInput.AadharNumber)).Returns(calc);

            repo.Setup(r => r.GetDisbursementMessage(input)).Returns(
                new HttpResponseMessage(HttpStatusCode.BadRequest));

            repo.Setup(r => r.CalculatePensionAmount(25000, 1000, 1, PensionType.Self)).Returns(21500.00);


            var result = processPensionController.ProcessPension(pensionerInput);


            Assert.That(result, Is.Not.InstanceOf<OkObjectResult>());

        }


    }
}
