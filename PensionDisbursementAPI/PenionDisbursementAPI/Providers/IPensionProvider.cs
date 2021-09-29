using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenionDisbursementAPI.Models;

namespace PenionDisbursementAPI.Providers
{
	public interface IPensionProvider
	{
		public PensionerDetail GetData(string AadharNumber);
	}
}
