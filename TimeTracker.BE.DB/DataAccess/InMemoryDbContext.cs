using Microsoft.EntityFrameworkCore;

namespace TimeTracker.BE.DB.DataAccess
{
	public class InMemoryDbContext : MainDatacontext
	{
		public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }

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
