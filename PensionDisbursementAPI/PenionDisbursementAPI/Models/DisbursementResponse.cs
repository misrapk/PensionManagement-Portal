using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenionDisbursementAPI.Models
{
	public class DisbursementResponse
	{
		public string Version { get; set; }
		public int Status { get; set; }
		public string message { get; set; }
	}
}
