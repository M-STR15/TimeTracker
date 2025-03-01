using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.PC.Factories
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
	{
		public SqliteDbContext CreateDbContext(string[] args)
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			var DbPath = Path.Join(path, "TimeTracker.db");

			//_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();
			// Načtení konfigurace
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			//var DbPath = configuration.GetSection("ConnectionString")["Database"];
			// Vytvoření DbContextOptions
			var options = new DbContextOptionsBuilder<MainDatacontext>()
				.UseSqlite($"Data Source={DbPath}")
				.Options;

			return new SqliteDbContext(options);
		}
	}
}
