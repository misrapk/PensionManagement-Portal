using Microsoft.EntityFrameworkCore;

namespace PensionManagementPortalAPP.Models
{
	public class PensionDbContext:DbContext
	{
		public PensionDbContext(DbContextOptions<PensionDbContext> options) : base(options)
		{

		}
		public DbSet<PensionDetail> Responses { get; set; }
	}
}
