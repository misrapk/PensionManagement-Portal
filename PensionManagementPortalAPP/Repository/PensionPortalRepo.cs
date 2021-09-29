using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PensionManagementPortalAPP.Models;

namespace PensionManagementPortalAPP.Repository
{
	public class PensionPortalRepo : IPensionPortalRepo
	{
		private PensionDbContext _context;
		public PensionPortalRepo(PensionDbContext context)
		{
			_context = context;
		}
		public void AddResponse(PensionDetail detail)
		{
			_context.Responses.Add(detail);
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
