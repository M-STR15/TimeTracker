using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTracker.BE.DB.DataAccess
{
	public class SqliteDbContext : MainDatacontext, IDesignTimeDbContextFactory<SqliteDbContext>
	{
		public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }


		public SqliteDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();

			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite($"Data Source={GetDatabasePath()}");
			}

			return new SqliteDbContext(optionsBuilder.Options);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite($"Data Source={GetDatabasePath()}");
			}
		}
		/// <summary>
		/// Vypíše cestu k databázi
		/// </summary>
		/// <returns></returns>
		public static string GetDatabasePath()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			var DbPath = Path.Join(path, "TimeTracker.db");
			return DbPath;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			_modelBuilder = modelBuilder;
			insertDefaultValues_Activities();
			insertDefaultValues_TypeShifts();

			setSubModuleTable();
		}
	}
}
