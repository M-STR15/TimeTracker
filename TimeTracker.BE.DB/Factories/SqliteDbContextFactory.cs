using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.DB.Factories
{
	public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
	{
		public SqliteDbContext CreateDbContext(string[] args)
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			var DbPath = Path.Join(path, "TimeTracker.db");

			var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite($"Data Source={DbPath}");
			}

			return new SqliteDbContext(optionsBuilder.Options);
		}


	}
}
