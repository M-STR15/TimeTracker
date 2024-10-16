using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TimerTracker.Models;

namespace TimerTracker.DataAccess
{
	internal class MainDatacontext : DbContext
	{
		public DbSet<Activity> Activities { get; set; }
		public DbSet<Project> Projects { get; set; }

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
	}
}
