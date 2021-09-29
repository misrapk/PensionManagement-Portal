using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PensionerDetailAPI.Models;

namespace PensionerDetailAPI.Repository
{
	public interface IPensionerDetail
	{
		public PensionerDetail PensionerDetailByAadhaar(string aadhar);
		public List<PensionerDetail> GetDetailsCSV();
	}
}
