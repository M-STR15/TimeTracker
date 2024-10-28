using TimerTracker.BE.DB.DataAccess;
using TimerTracker.BE.DB.Models;

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