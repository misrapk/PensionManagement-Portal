using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension_API.Models
{
	public class ProcessPensionInput
	{
		public string AadharNumber { get; set; }
		public double PensionAmount { get; set; }
		public int BankCharge { get; set; }
	}
	public enum PensionTypeValue
	{
		Self = 1,
		Family =2
	}
	public class ProcessResponse
	{
		public int Status { get; set; }
		public double PensionAmount { get; set; }
	}
	public class ResultforValueCalculation
	{
		public string message { get; set; }
		public ValueforCalculation result { get; set; }
	}
}
