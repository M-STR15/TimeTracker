using Microsoft.EntityFrameworkCore;
using TimerTracker.DataAccess;
using TimerTracker.Models;

namespace TimerTracker.Providers
{
	public class DatabaseProvider
	{
		private readonly MainDatacontext _mainDatacontext;
		public DatabaseProvider(MainDatacontext mainDatacontext)
		{
			_mainDatacontext = mainDatacontext;
		}

		public List<Activity> GetActivities()
		{
			try
			{
				return _mainDatacontext.Activities.ToList();
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
				return _mainDatacontext.Projects.ToList();
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
				var recordActivities = _mainDatacontext.RecordActivities
					.Include(x => x.Project)
					.Include(x => x.Activity)
					.OrderBy(x => x.StartTime).ToList();

				return recordActivities;
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
				_mainDatacontext.RecordActivities.Add(recordActivity);
				_mainDatacontext.SaveChanges();

			}
			catch (Exception)
			{

			}
		}
	}
}
