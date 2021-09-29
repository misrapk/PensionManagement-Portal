using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenionDisbursementAPI.Models;

namespace PenionDisbursementAPI.Repository
{
	public interface IPensionerRepo
	{
		public PensionerDetail GetDetail(string AadharNumber);
		public int Status(PensionerDetail pensionerDetail, ProcessPensionInput input);
	}
}
