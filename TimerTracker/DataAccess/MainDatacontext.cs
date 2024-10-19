using Microsoft.EntityFrameworkCore;
using TimerTracker.Models.Database;

namespace TimerTracker.DataAccess
{
    public class MainDatacontext : DbContext
	{
		private ModelBuilder _modelBuilder { get; set; }

		public DbSet<Activity> Activities { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<RecordActivity> RecordActivities { get; set; }
		public DbSet<Shift> Shifts { get; set; }
		public string DbPath { get; }

		public MainDatacontext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "timerTracker.db");
		}

		// The following configures EF to create a Sqlite database file in the
		// special "local" folder for your platform.
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			_modelBuilder = modelBuilder;
			insertDefaultValues_Activities();
			insertDefaultValues_Projects();
		}

		private void insertDefaultValues_Activities()
		{
			_modelBuilder.Entity<Activity>()
				.HasData(
				new Activity((int)eActivity.Start, eActivity.Start.ToString()),
				new Activity((int)eActivity.Pause, eActivity.Pause.ToString()),
				new Activity((int)eActivity.Stop, eActivity.Stop.ToString())
				);
		}

		private void insertDefaultValues_Projects()
		{
			_modelBuilder.Entity<Project>()
				.HasData(
				new Project(1, "Project 1"),
				new Project(2, "Project 2")
				);
		}
	}
}
