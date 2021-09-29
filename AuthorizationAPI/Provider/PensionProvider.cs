using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Models;

namespace AuthorizationAPI.Provider
{
	public class PensionProvider : IPensionProvider
	{
		private static List<PensionCreds> List = new List<PensionCreds>()
		{
			new PensionCreds
			{
				Username = "567488589635",
				Password = "pass1"
			},
			new PensionCreds
			{
				Username = "user2",
				Password = "pass2"
			},
			new PensionCreds
			{
				Username = "111122223333",
				Password = "pass2"
			},
            new PensionCreds {
                Username = "111122223333",
                Password = "pass1"
            },
            new PensionCreds
            {
                Username = "222233334444",
                Password = "pass1"
            },
            new PensionCreds
            {
                Username = "212122223333",
                Password = "pass1"
             },
            new PensionCreds
            {
                Username = "511122223331",
                Password = "pass1"
            },
            new PensionCreds
            {
                Username = "876594358734",
                Password = "pass1"
            },
            new PensionCreds
            {
                Username = "111122229999",
                Password = "pass1"
            },
            new PensionCreds
            {
                Username = "987493726123",
                Password = "pass1"
            }
        };
		public List<PensionCreds> GetList()
		{
			return List;
		}

		public PensionCreds GetPensioner(PensionCreds cred)
		{
			List<PensionCreds> list = GetList();
			PensionCreds pensionCred = list.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);

			return pensionCred;
		}
	}
}
