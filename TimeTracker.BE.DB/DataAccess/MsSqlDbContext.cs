using Microsoft.EntityFrameworkCore;

namespace TimeTracker.BE.DB.DataAccess
{
	public class MsSqlDbContext : MainDatacontext
	{
		public MsSqlDbContext(DbContextOptions<MainDatacontext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("server=DESKTOP-JS0N1LD\\SQLEXPRESS; database=TimeTracker;Trusted_Connection=True;TrustServerCertificate=True;");
			}
		}
	}
}
