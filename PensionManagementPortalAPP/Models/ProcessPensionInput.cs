using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortalAPP.Models
{
	public class ProcessPensionInput
	{
		public long AadharNumber { get; set; }
		public double PensionAmount { get; set; }
		public int BankCharge { get; set; }
	}

    public class ProcessPensionResponse
    {

        public int Status { get; set; }
        public double PensionAmount { get; set; }
    }

    public class ProcessResponse
    {
        public string Message { get; set; }
        public ProcessPensionResponse Result { get; set; }
    }
}
