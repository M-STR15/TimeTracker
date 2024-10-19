using TimerTracker.DataAccess;
using TimerTracker.Models.Database;

namespace TimerTracker.Providers
{
    public class ActivityProvider
	{
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
	}
}
