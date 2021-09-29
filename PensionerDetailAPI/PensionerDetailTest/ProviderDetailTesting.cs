using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;
//using PensionerDetailAPI.Controllers;
using PensionerDetailAPI.Models;
using PensionerDetailAPI.Provider;

namespace PensionerDetailTest
{
	public class ProviderDetailTesting
	{
		List<PensionerDetail> details;
		[SetUp]

		public void Setup()
		{
			details = new List<PensionerDetail>()
			{
				new PensionerDetail()
				{
					Name = "testUser",
					DateofBirth = Convert.ToDateTime("1998-03-01"),
					PAN = "BCFPN1234F",
					AadharNumber = "123456789012",
					SalaryEarned = 400000,
					Allowances = 5000,
					PensionType = (PensionTypeValue)(2),
					BankName ="HDFC",
					AccountNumber = "123456789876",
					BankType = (BankType)(2)
				}
			};
		}

		[TestCase("567489032345")]
		public void GetDetailsByAadhar_Returns_ListObject(string aadhar)
		{

			Mock<IPensionerDetailProvider> mock = new();

			mock.Setup(p => p.GetDetailsByAadhar(aadhar)).Returns(details[0]);

			PensionerDetail det = mock.Object.GetDetailsByAadhar(aadhar);

			Assert.AreEqual(det, details[0]);
		}

		[TestCase("111122223333")]
		public void GetDetailsByAadhar_Returns_Null(string aadhar)
		{
			Mock<IPensionerDetailProvider> mock = new();

			mock.Setup(p => p.GetDetailsByAadhar(aadhar)).Returns(new PensionerDetail());

			PensionerDetail det = mock.Object.GetDetailsByAadhar(aadhar);

			Assert.AreNotEqual(det, details[0]);
		}
	}
}