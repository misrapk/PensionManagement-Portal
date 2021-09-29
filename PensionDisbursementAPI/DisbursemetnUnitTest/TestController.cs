using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PenionDisbursementAPI.Controllers;
using PenionDisbursementAPI.Models;
using PenionDisbursementAPI.Providers;
using PenionDisbursementAPI.Repository;

namespace DisbursemetnUnitTest
{
	[TestFixture]
	public class TestController
	{
		Mock<IPensionerRepo> repoMock;

		DisbursementController controller;
		PensionerDetail pensionerDetail;
		PensionerRepo repo;
		IPensionProvider data;

		[SetUp]
		public void Setup()
		{
			repoMock = new Mock<IPensionerRepo>();
			controller = new DisbursementController(repoMock.Object);
			data = new PensionProvider();
			repo = new PensionerRepo(data);
			pensionerDetail = new PensionerDetail

			{
				Name = "Dipika",
				DateofBirth = Convert.ToDateTime("1998-03-01"),
				PAN = "BCFPN1234F",
				AadharNumber = "111122223333",
				SalaryEarned = 40000,
				Allowances = 5000,
				PensionType = (PensionTypeValue)(2),
				BankName = "HDFC",
				AccountNumber = "123456789876",
				BankType = (BankType)(2)
			};
		}

		[TestCase("111122223333")]
		public void DisbursementController_ValidAadhaar_return_OK10(string aadhaar)
		{
			ProcessPensionInput pensionInput = new ProcessPensionInput 
			{
				AadharNumber = aadhaar,
				BankCharge = 550, 
				PensionAmount = 24450.0 
			};
			repoMock.Setup(p => p.GetDetail(aadhaar)).Returns(pensionerDetail);

			repoMock.Setup(p => p.Status(pensionerDetail, pensionInput)).Returns(repo.Status(pensionerDetail, pensionInput));
			var response = controller.DisbursePension(pensionInput);
			OkObjectResult okresult = response as OkObjectResult;
			Assert.AreEqual(200, okresult.StatusCode);

		}

		[TestCase("111122220000")]
		public void DisbursementController_InvalidAadhar_return_Error21(string aadhaar)
		{
			ProcessPensionInput pensionInput = new ProcessPensionInput
			{
				AadharNumber = aadhaar,
				BankCharge = 550,
				PensionAmount = 24450.0
			};
			repoMock.Setup(p => p.GetDetail(aadhaar)).Returns(pensionerDetail);

			repoMock.Setup(p => p.Status(pensionerDetail, pensionInput)).Returns(repo.Status(pensionerDetail, pensionInput));
			var response = controller.DisbursePension(pensionInput);
			OkObjectResult okresult = response as OkObjectResult;
			Assert.AreEqual(200, okresult.StatusCode);

		}


		//pension Amount Test
		[TestCase("2440.0")]
		public void DisbursementController_Invalid_PensionAmount_return_ErrorValue_21(double pension)
		{
			ProcessPensionInput pensionInput = new ProcessPensionInput { AadharNumber = "111122223333", BankCharge = 550, PensionAmount = pension };
			repoMock.Setup(p => p.GetDetail(pensionInput.AadharNumber)).Returns(pensionerDetail);

			repoMock.Setup(p => p.Status(pensionerDetail, pensionInput)).Returns(repo.Status(pensionerDetail, pensionInput));
			var response = controller.DisbursePension(pensionInput);
			OkObjectResult okresult = response as OkObjectResult;
			Assert.AreEqual(200, okresult.StatusCode);
		}
	}
}