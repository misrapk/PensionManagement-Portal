using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenionDisbursementAPI.Models
{
	public class ProcessPensionInput
	{
		[RegularExpression(@"^[0-9]{12}$", ErrorMessage = "Enter Valid Aadhar Number")]
		public string AadharNumber { get; set; }
		public double PensionAmount { get; set; }
		public int BankCharge { get; set; }
	}
}
