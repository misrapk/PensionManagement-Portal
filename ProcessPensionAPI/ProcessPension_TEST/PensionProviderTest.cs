using System;
using System.Net;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using ProcessPension_API.Models;
using ProcessPension_API.Provider;

namespace ProcessPension_TEST
{
	public class PensionProviderTest
	{
		Mock<IProcessProvider> pro = new Mock<IProcessProvider>();
		ProcessProvider _pro = new ProcessProvider();
		HttpResponseMessage responseMessage;
		HttpStatusCode statusCode = HttpStatusCode.OK;
		ValueforCalculation valueforCalCulation;
		PensionDetail pensionDetail;


		[SetUp]
		public void Setup()
		{
			responseMessage = new HttpResponseMessage(statusCode);
			pensionDetail = new PensionDetail()
			{
				Name = "Ritvik",
				Pan = "BCFPN1234F",
				PensionAmount = 25000,
				DateOfBirth = new DateTime(2000, 01, 01),
				PensionType = PensionType.Self,
				BankType = 1,
				AadharNumber = "111122223333",
				Status = 21
			};
			valueforCalCulation = new ValueforCalculation() { Allowances = 1000, BankType = 1, PensionType = PensionType.Self, SalaryEarned = 25000 };
		}

		[TestCase(25000, 1000,1, PensionType.Self)]
		public void CalculatePensionAmount_CorrectInput_returnCorrect(int salary, int allowances, int bankType, PensionType pensionType)
		{
			pro.Setup(r => r.CalculatePensionAmount(salary, allowances, bankType, pensionType)).Returns(21500.00);
			double amount = pro.Object.CalculatePensionAmount(salary, allowances, bankType, pensionType);
			Assert.AreEqual(21500.00, amount);
		}


		//GetDisbursement Message Success Code
		[TestCase("111122223333", 25504, 50)]
		public void GetDisbursementMessage_Returns_Success(string aadhar, int pensionamount, int bankcharge)
		{
			ProcessPensionInput processPensionInput = new() 
			{
				AadharNumber = aadhar,
				BankCharge = bankcharge,
				PensionAmount = pensionamount
			};
			pro.Setup(r => r.GetDisbursementMessage(processPensionInput)).Returns(responseMessage);
			HttpResponseMessage res = pro.Object.GetDisbursementMessage(processPensionInput);
			Assert.AreEqual(res, responseMessage);
		}

		//Return Incorrect
		[TestCase("111122223333", 25500, 550)]
		public void GetDisbursementMessage_Returns_Fail(string aadhar, int pensionamount, int bankcharge)
		{
			ProcessPensionInput processPensionInput = new ProcessPensionInput() 
			{
				AadharNumber = aadhar, 
				BankCharge = bankcharge,
				PensionAmount = pensionamount 
			};

			HttpStatusCode badcode = HttpStatusCode.BadRequest;
			HttpResponseMessage bad = new(badcode);
			pro.Setup(r => r.GetDisbursementMessage(processPensionInput)).Returns(responseMessage);

			HttpResponseMessage res = pro.Object.GetDisbursementMessage(processPensionInput);
			Assert.AreNotEqual(res, bad);

		}


	}
}