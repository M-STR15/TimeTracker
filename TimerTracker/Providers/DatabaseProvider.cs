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
	}
}
