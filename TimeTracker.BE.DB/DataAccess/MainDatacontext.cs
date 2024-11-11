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

            testDataRecordActivities();
        }

        private void testDataRecordActivities()
        {
            var year=DateTime.Now.Year;
            var month=DateTime.Now.Month;

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