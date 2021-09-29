using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Models;

namespace AuthorizationAPI.Provider
{
	public interface IPensionProvider
	{
		public List<PensionCreds> GetList();
		public PensionCreds GetPensioner(PensionCreds cred);
	}
}
