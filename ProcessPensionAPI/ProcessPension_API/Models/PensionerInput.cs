using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension_API.Models
{
	public class PensionerInput
	{
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PAN { get; set; }
		public string AadharNumber { get; set; }
		public PensionType PensionType { get; set; }
	}
	public enum PensionType
	{
		Self = 1,
		Family = 2
	}
}
