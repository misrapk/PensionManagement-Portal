using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Models;
using AuthorizationAPI.Provider;

namespace AuthorizationAPI.Repository
{
	public class PensionRepo : IPensionRepo
	{
		private IPensionProvider _provider;
		public PensionRepo(IPensionProvider provider)
		{
			_provider = provider;
		}
		public PensionCreds GetPensionerCredentials(PensionCreds cred)
		{
			if (cred == null) return null;
			PensionCreds pensioner = _provider.GetPensioner(cred);

			return pensioner;
		}
	}
}
