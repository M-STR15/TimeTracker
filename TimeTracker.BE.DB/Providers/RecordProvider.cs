using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.BE.DB.Providers
{
    public class RecordProvider
    {
        public RecordProvider()
        {
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

        public RecordActivity SaveRecord(RecordActivity recordActivity)
        {
            try
            {
                using (var context = new MainDatacontext())
                {
                    context.RecordActivities.Add(recordActivity);
                    context.SaveChanges();
                }
                return recordActivity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}