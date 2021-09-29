using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension_API.Models
{
	public class PensionDetail
	{
        public string Name { get; set; }
        public double PensionAmount { get; set; }
        public string Pan { get; set; }
        public string AadharNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PensionType PensionType { get; set; }
        public int BankType { get; set; }
        public int Status { get; set; }
    }
}
