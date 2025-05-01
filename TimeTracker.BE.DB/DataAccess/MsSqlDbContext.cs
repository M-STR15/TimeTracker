using Microsoft.EntityFrameworkCore;

namespace TimeTracker.BE.DB.DataAccess
{
	public class MsSqlDbContext : MainDatacontext
	{
		public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Server=DESKTOP-JS0N1LD\SQLEXPRESS;Integrated Security=true;TrustServerCertificate=true;Database=TimeTrackerDB");
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
