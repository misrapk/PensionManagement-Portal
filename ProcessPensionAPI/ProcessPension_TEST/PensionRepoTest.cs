using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ProcessPension_API.Models;
using ProcessPension_API.Repository;

namespace ProcessPension_TEST
{
	public class PensionRepoTest
	{
        Mock<IProcessRepo> pro = new Mock<IProcessRepo>();

        HttpResponseMessage responseMessage;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        ValueforCalculation valueforCalculation;
        PensionDetail pensionDetail;

        [SetUp]
        public void Setup()
        {
            //pro = new Mock<IProcessProvider>();
            responseMessage = new HttpResponseMessage(statusCode);
            pensionDetail = new PensionDetail()
            {
                Name = "Satpreet",
                Pan = "BCFPN1234F",
                PensionAmount = 25000,
                DateOfBirth = new DateTime(2000, 01, 01),
                PensionType = PensionType.Self,
                BankType = 1,
                AadharNumber = "111122223333",
                Status = 21
            };
            valueforCalculation = new ValueforCalculation() { Allowances = 1000, BankType = 1, PensionType = PensionType.Self, SalaryEarned = 25000 };
        }


        [TestCase("111122223333", 25500, 550)]
        public void GetDisbursementMessage_Returns_Success(string aadhar, int pensionamount, int bankcharge)
        {
            ProcessPensionInput processPensionInput = new ProcessPensionInput() { AadharNumber = aadhar, BankCharge = bankcharge, PensionAmount = pensionamount };
            pro.Setup(r => r.GetDisbursementMessage(processPensionInput)).Returns(responseMessage);
            HttpResponseMessage res = pro.Object.GetDisbursementMessage(processPensionInput);
            Assert.AreEqual(res, responseMessage);
        }

        [TestCase("111122223333", 25500, 550)]
        public void GetDisbursementMessage_Returns_Fail(string aadhar, int pensionamount, int bankcharge)
        {
            ProcessPensionInput processPensionInput = new ProcessPensionInput() { AadharNumber = aadhar, BankCharge = bankcharge, PensionAmount = pensionamount };

            HttpStatusCode badcode = HttpStatusCode.BadRequest;
            HttpResponseMessage bad = new HttpResponseMessage(badcode);
            pro.Setup(r => r.GetDisbursementMessage(processPensionInput)).Returns(responseMessage);

            HttpResponseMessage res = pro.Object.GetDisbursementMessage(processPensionInput);
            Assert.AreNotEqual(res, bad);

        }

        [TestCase("111122223333")]
        public void GetCalculationValues_Returns_CorrectValues(string aadhar)
        {
            pro.Setup(r => r.GetCalculationValues(aadhar)).Returns(valueforCalculation);
            ValueforCalculation values = pro.Object.GetCalculationValues(aadhar);
            Assert.AreEqual(values, valueforCalculation);
        }

        [TestCase("111122223333")]
        public void GetCalculationValues_Returns_IncorrectValues(string aadhar)
        {
            pro.Setup(r => r.GetCalculationValues(aadhar)).Returns(valueforCalculation);
            ValueforCalculation values = pro.Object.GetCalculationValues(aadhar);
            Assert.AreNotEqual(values, null);
        }

        [TestCase("111122223333")]
        public void GetClientInfo_Returns_Correct_PensionDetails(string aadhar)
        {
            pro.Setup(r => r.GetClientInfo(aadhar)).Returns(pensionDetail);
            PensionDetail detail = pro.Object.GetClientInfo(aadhar);
            Assert.AreEqual(detail, pensionDetail);
        }

        [TestCase("111122223333")]
        public void GetClientInfo_Returns_Incorrect_PensionDetails(string aadhar)
        {
            pro.Setup(r => r.GetClientInfo(aadhar)).Returns(pensionDetail);
            PensionDetail detail = pro.Object.GetClientInfo(aadhar);
            Assert.AreNotEqual(detail, null);
        }

        [TestCase(25000, 1000, 1, PensionType.Self)]
        public void CalculatePensionAmount_Returns_CorrectValue(int salary, int allowances, int bankType, PensionType pensionType)
        {
            pro.Setup(r => r.CalculatePensionAmount(salary, allowances, bankType, pensionType)).Returns(21500.00);
            double amount = pro.Object.CalculatePensionAmount(salary, allowances, bankType, pensionType);
            Assert.AreEqual(21500.00, amount);

        }

        [TestCase(25000, 1000, 1, PensionType.Self)]
        public void CalculatePensionAmount_Returns_IncorrectValue(int salary, int allowances, int bankType, PensionType pensionType)
        {
            pro.Setup(r => r.CalculatePensionAmount(salary, allowances, bankType, pensionType)).Returns(21500.00);
            double amount = pro.Object.CalculatePensionAmount(salary, allowances, bankType, pensionType);
            Assert.AreNotEqual(21000.00, amount);

        }

    }
}
