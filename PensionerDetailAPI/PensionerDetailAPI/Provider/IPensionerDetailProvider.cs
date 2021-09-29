using PensionerDetailAPI.Models;

namespace PensionerDetailAPI.Provider
{
	public interface IPensionerDetailProvider
	{
		public PensionerDetail GetDetailsByAadhar(string aadhar);
	}
}