using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTracker.BE.DB.DataAccess
{
	public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
	{
		public SqliteDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();

			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			var dbPath = Path.Join(path, "TimeTracker.db");

			optionsBuilder.UseSqlite($"Data Source={dbPath}");

			return new SqliteDbContext(optionsBuilder.Options);
		}
	}
}
