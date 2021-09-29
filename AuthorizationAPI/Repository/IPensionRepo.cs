using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Models;

namespace AuthorizationAPI.Repository
{
	public interface IPensionRepo
	{
		public PensionCreds GetPensionerCredentials(PensionCreds cred);
	}
}
