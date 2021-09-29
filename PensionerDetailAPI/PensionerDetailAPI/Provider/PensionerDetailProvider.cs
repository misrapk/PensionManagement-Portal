using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PensionerDetailAPI.Models;
using PensionerDetailAPI.Repository;

namespace PensionerDetailAPI.Provider
{
	public class PensionerDetailProvider : IPensionerDetailProvider
	{
		private IPensionerDetail _detail;
		public PensionerDetail GetDetailsByAadhar(string aadhar)
		{
			_detail = new PensionerDetailRepo();

			PensionerDetail pensioner = _detail.PensionerDetailByAadhaar(aadhar);
			return pensioner; 
		}
	}
}
