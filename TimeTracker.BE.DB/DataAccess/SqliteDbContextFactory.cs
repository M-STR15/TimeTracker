using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTracker.BE.DB.DataAccess
{
	public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
	{
		public SqliteDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
			optionsBuilder.UseSqlite($"Data Source={SqliteDbContext.GetDatabasePath()}");

			return new SqliteDbContext(optionsBuilder.Options);
		}
	}
}
