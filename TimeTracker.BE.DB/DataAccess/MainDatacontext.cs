using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;

namespace TimeTracker.BE.DB.DataAccess
{
	public class MainDatacontext : DbContext
	{
		private ModelBuilder _modelBuilder { get; set; }

		public DbSet<Activity> Activities { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<RecordActivity> RecordActivities { get; set; }
		public DbSet<Shift> Shifts { get; set; }
		public DbSet<SubModule> SubModules { get; set; }
		public DbSet<TypeShift> TypeShifts { get; set; }
		public string DbPath { get; }

		public MainDatacontext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "TimeTracker.db");
		}

		// The following configures EF to create a Sqlite database file in the
		// special "local" folder for your platform.
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			_modelBuilder = modelBuilder;
			insertDefaultValues_Activities();
			insertDefaultValues_TypeShifts();
			insertDefaultValues_Projects();

			setSubModuleTable();

			createTestData();
		}

		private void createTestData()
		{
			createTestDataRecordActivities();
			createTestDataShifts();
		}

		private void createTestDataRecordActivities()
		{
			var year = DateTime.Now.Year;
			var month = DateTime.Now.Month;

			_modelBuilder.Entity<RecordActivity>()
			.HasData(
			  new RecordActivity(new DateTime(year, month, 1, 7, 0, 0), new DateTime(year, month, 1, 11, 30, 0), (int)eActivity.Start, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 1, 11, 30, 0), new DateTime(year, month, 1, 12, 0, 0), (int)eActivity.Pause, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 1, 12, 0, 0), new DateTime(year, month, 1, 15, 0, 0), (int)eActivity.Start, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 1, 15, 0, 0), (int)eActivity.Stop, (int)eTypeShift.Office),

			  new RecordActivity(new DateTime(year, month, 2, 7, 0, 0), new DateTime(year, month, 2, 11, 30, 0), (int)eActivity.Start, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 2, 11, 30, 0), new DateTime(year, month, 2, 12, 0, 0), (int)eActivity.Pause, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 2, 12, 0, 0), new DateTime(year, month, 2, 15, 0, 0), (int)eActivity.Start, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 2, 15, 0, 0), (int)eActivity.Stop, (int)eTypeShift.Office),

			  new RecordActivity(new DateTime(year, month, 3, 7, 0, 0), new DateTime(year, month, 3, 11, 30, 0), (int)eActivity.Start, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 3, 11, 30, 0), new DateTime(year, month, 3, 12, 0, 0), (int)eActivity.Pause, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 3, 12, 0, 0), new DateTime(year, month, 3, 15, 0, 0), (int)eActivity.Start, (int)eTypeShift.Office),
			  new RecordActivity(new DateTime(year, month, 3, 15, 0, 0), (int)eActivity.Stop, (int)eTypeShift.Office),

			  new RecordActivity(new DateTime(year, month, 4, 7, 0, 0), new DateTime(year, month, 4, 11, 40, 0), (int)eActivity.Start, (int)eTypeShift.HomeOffice),
			  new RecordActivity(new DateTime(year, month, 4, 11, 40, 0), new DateTime(year, month, 4, 12, 0, 0), (int)eActivity.Pause, (int)eTypeShift.HomeOffice),
			  new RecordActivity(new DateTime(year, month, 4, 12, 0, 0), new DateTime(year, month, 4, 15, 10, 0), (int)eActivity.Start, (int)eTypeShift.HomeOffice),
			  new RecordActivity(new DateTime(year, month, 4, 15, 10, 0), (int)eActivity.Stop, (int)eTypeShift.HomeOffice),

			  new RecordActivity(new DateTime(year, month, 5, 8, 0, 0), new DateTime(year, month, 5, 11, 40, 0), (int)eActivity.Start, (int)eTypeShift.Others),
			  new RecordActivity(new DateTime(year, month, 5, 11, 40, 0), new DateTime(year, month, 5, 12, 0, 0), (int)eActivity.Pause, (int)eTypeShift.Others),
			  new RecordActivity(new DateTime(year, month, 5, 12, 0, 0), new DateTime(year, month, 5, 16, 0, 0), (int)eActivity.Start, (int)eTypeShift.Others),
			  new RecordActivity(new DateTime(year, month, 5, 16, 0, 0), (int)eActivity.Stop, (int)eTypeShift.Others)
			);
		}

		private void createTestDataShifts()
		{
			var year = DateTime.Now.Year;
			var month = DateTime.Now.Month;
			_modelBuilder.Entity<Shift>()
			.HasData(
						new Shift(Guid.Parse("fa1db3cb-f2c4-4efd-aee9-cd3487366229"), new DateTime(year, month, 1), (int)eTypeShift.Office),
						new Shift(Guid.Parse("9ad8ec6d-6bd8-4cf5-b681-c46c86c508f3"), new DateTime(year, month, 2), (int)eTypeShift.Office),
						new Shift(Guid.Parse("d917a220-5a2c-401c-90cc-c746aaada412"), new DateTime(year, month, 3), (int)eTypeShift.HomeOffice),
						new Shift(Guid.Parse("68dbc51a-9546-4450-bf46-e614397021e4"), new DateTime(year, month, 4), (int)eTypeShift.HomeOffice),
						new Shift(Guid.Parse("31402227-1064-4455-872f-df218a85aca3"), new DateTime(year, month, 5), (int)eTypeShift.Office),
						new Shift(Guid.Parse("d434a034-f93d-4f68-a69a-60243a16d21d"), new DateTime(year, month, 8), (int)eTypeShift.Office),
						new Shift(Guid.Parse("a634ce0f-ab0c-4061-babb-70f656277fa1"), new DateTime(year, month, 9), (int)eTypeShift.Others),
						new Shift(Guid.Parse("865370c2-1115-4b23-b4c9-f7cdebbbe86d"), new DateTime(year, month, 10), (int)eTypeShift.Office),
						new Shift(Guid.Parse("a9f6f060-c255-43e5-b5d5-e9d7860ab14d"), new DateTime(year, month, 11), (int)eTypeShift.HomeOffice),
						new Shift(Guid.Parse("d10f4bf4-c1a4-404b-9213-803ad4cee509"), new DateTime(year, month, 12), (int)eTypeShift.Office)
			);
		}

		private void setSubModuleTable()
		{
			_modelBuilder.Entity<SubModule>()
				.HasOne(p => p.Project)
				.WithMany(b => b.SubModules)
				.HasForeignKey(p => p.ProjectId)
				.OnDelete(DeleteBehavior.Cascade);
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

		private void insertDefaultValues_TypeShifts()
		{
			_modelBuilder.Entity<TypeShift>()
				.HasData(
				new TypeShift((int)eTypeShift.HomeOffice, eTypeShift.HomeOffice.ToString(), "SkyBlue"),
				new TypeShift((int)eTypeShift.Office, eTypeShift.Office.ToString(), "Orange"),
				new TypeShift((int)eTypeShift.Others, eTypeShift.Others.ToString(), "Magenta"),
				new TypeShift((int)eTypeShift.Holiday, eTypeShift.Holiday.ToString(), "LawnGreen", false)
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