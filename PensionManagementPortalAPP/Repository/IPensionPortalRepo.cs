using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PensionManagementPortalAPP.Models;

namespace PensionManagementPortalAPP.Repository
{
	public interface IPensionPortalRepo
	{
		public void AddResponse(PensionDetail detail);
		public void Save();
	}
}
