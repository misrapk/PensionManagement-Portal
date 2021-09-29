using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenionDisbursementAPI.Models;
using PenionDisbursementAPI.Providers;

namespace PenionDisbursementAPI.Repository
{
	public class PensionerRepo : IPensionerRepo
	{
		private readonly IPensionProvider _provider;
		ProcessPensionResponse processPensionResponse = new ProcessPensionResponse();

		public PensionerRepo(IPensionProvider provider)
		{
			_provider = provider;
		}
		public PensionerDetail GetDetail(string AadharNumber)
		{
			PensionerDetail pensionerDetail = null;
			pensionerDetail = _provider.GetData(AadharNumber);
			return pensionerDetail;
		}

		public int Status(PensionerDetail pensionerDetail, ProcessPensionInput input)
		{
			if (input.PensionAmount == 0) return 21;
			if (input.BankCharge == 0 || (pensionerDetail.BankType == BankType.Private && input.BankCharge != 550) || (pensionerDetail.BankType == BankType.Public && input.BankCharge != 500)) return 21;

			double rate = (pensionerDetail.PensionType == PensionTypeValue.Family) ? 0.5 : 0.8;

			double pension = (rate * pensionerDetail.SalaryEarned) + pensionerDetail.Allowances + input.BankCharge;

			if (pension != input.PensionAmount) return 21;

			return 10;
		}
	}
}
