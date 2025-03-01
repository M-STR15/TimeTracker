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
				optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=TimeTracker;Trusted_Connection=True;TrustServerCertificate=True;");
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
