using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProcessPension_API.Models;
using ProcessPension_API.Provider;

namespace ProcessPension_API.Repository
{
	public class ProcessRepo : IProcessRepo
	{
		private IProcessProvider _provider;

		public ProcessRepo(IProcessProvider provider)
		{
			_provider = provider;
		}
		public double CalculatePensionAmount(int salary, int allowances, int bankType, PensionType pensionType)
		{
			double pensionAmount;
			pensionAmount = _provider.CalculatePensionAmount(salary, allowances, bankType, pensionType);
			return pensionAmount;
		}

		public ValueforCalculation GetCalculationValues(string aadhar)
		{
			ValueforCalculation value = _provider.GetCalculationValues(aadhar);
			return value;
		}

		public PensionDetail GetClientInfo(string aadhar)
		{
			PensionDetail pensionDetail = _provider.GetClientInfo(aadhar);
			return pensionDetail;
		}

		public HttpResponseMessage GetDisbursementMessage(ProcessPensionInput pensionDetail)
		{
			HttpResponseMessage res = _provider.GetDisbursementMessage(pensionDetail);

			return res;
		}
	}
}
