using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.BE.DB.Providers
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