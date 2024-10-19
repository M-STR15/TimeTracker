using Microsoft.EntityFrameworkCore;
using TimerTracker.DataAccess;
using TimerTracker.Models;

namespace TimerTracker.Providers
{
	public class DatabaseProvider
	{
		public DatabaseProvider()
		{
		}

		public List<Activity> GetActivities()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					return context.Activities.ToList();
				}
			}
			catch (Exception)
			{
				return new();
			}
		}

		public List<Project> GetProjects()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					return context.Projects.ToList();
				}
			}
			catch (Exception)
			{
				return new();
			}
		}

		public List<RecordActivity> GetRecords()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var recordActivities = context.RecordActivities
					.Include(x => x.Project)
					.Include(x => x.Activity)
					.OrderBy(x => x.StartTime).ToList();

					return recordActivities;
				}
			}
			catch (Exception)
			{
				return new();
			}
		}

		public void SaveRecord(RecordActivity recordActivity)
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					context.RecordActivities.Add(recordActivity);
					context.SaveChanges();
				}
			}
			catch (Exception)
			{

			}
		}
	}
}
