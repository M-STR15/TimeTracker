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
			return _mainDatacontext.Activities.ToList();
		}

		public List<Project> GetProjects()
		{
			return _mainDatacontext.Projects.ToList();
		}

		public List<RecordActivity> GetRecords()
		{
			var recordActivities = _mainDatacontext.RecordActivities
				.Include(x => x.Project)
				.Include(x => x.Activity)
				.OrderBy(x=>x.StartTime).ToList();

			return recordActivities;
		}

		public void SaveRecord(RecordActivity recordActivity)
		{
			_mainDatacontext.RecordActivities.Add(recordActivity);
			_mainDatacontext.SaveChanges();
		}
	}
}
