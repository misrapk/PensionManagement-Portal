using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProcessPension_API.Models;

namespace ProcessPension_API.Provider
{
	public interface IProcessProvider
	{
		public HttpResponseMessage PensionDetail(string aadhar);

		public PensionDetail GetClientInfo(string aadhar);

		public ValueforCalculation GetCalculationValues(string aadhar);

		public HttpResponseMessage GetDisbursementMessage(ProcessPensionInput pensionDetail);

		public double CalculatePensionAmount(int salary, int allowances, int bankType, PensionType pensionType);
	}
}
