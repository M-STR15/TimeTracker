using TimerTracker.BE.DB.Models;
using TimerTracker.BE.DB.DataAccess;

namespace TimerTracker.BE.DB.Providers
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
