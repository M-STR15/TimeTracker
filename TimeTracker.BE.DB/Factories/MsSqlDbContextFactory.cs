using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.DB.Factories
{
	public class MsSqlDbContextFactory : IDesignTimeDbContextFactory<MsSqlDbContext>
	{
		public MsSqlDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<MsSqlDbContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TimeTracker;Trusted_Connection=True;");

			return new MsSqlDbContext(optionsBuilder.Options);
		}
	}
}
