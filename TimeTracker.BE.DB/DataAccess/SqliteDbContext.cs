using Microsoft.EntityFrameworkCore;

namespace TimeTracker.BE.DB.DataAccess
{
	public class SqliteDbContext : MainDatacontext
	{
		public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			var DbPath = Path.Join(path, "TimeTracker.db");

			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite($"Data Source={DbPath}");
			}
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
