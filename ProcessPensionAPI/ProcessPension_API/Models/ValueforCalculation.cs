using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension_API.Models
{
	public class ValueforCalculation
	{
		public int BankType { get; set; }
		public int SalaryEarned { get; set; }
		public int Allowances { get; set; }
		public PensionType PensionType { get; set; }
	}
}
