using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;

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

        public List<RecordActivity> GetRecords(DateTime startTime, DateTime endTime)
        {
            try
            {
                using (var context = new MainDatacontext())
                {
                    var recordActivities = context.RecordActivities.Where(x => x.StartTime >= startTime && x.EndTime <= endTime)
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

        public RecordActivity GetLastRecordActivity()
        {
            RecordActivity? recordActivity = null;

            using (var context = new MainDatacontext())
            {
                recordActivity = context.RecordActivities.OrderBy(x => x.StartTime)
                    .Include(x => x.Project)
                    .Include(x => x.Activity)
                    .Include(x => x.Shift)
                    .Include(x => x.SubModule)
                    .Include(x => x.TypeShift)
                    .Last();
            }

            return recordActivity;
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

                UpdateRefreshEndTime();

                return recordActivity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void UpdateRefreshEndTime()
        {
            try
            {
                using (var context = new MainDatacontext())
                {
                    var recordActivities = context.RecordActivities.OrderBy(x => x.StartTime).ToList();

                    //foreach (var recordActivity in recordActivities)
                    //{
                    //    recordActivity.EndTime=
                    //}

                    var allowedActivities = new List<int>();
                    allowedActivities.Add((int)eActivity.Start);
                    allowedActivities.Add((int)eActivity.Pause);

                    for (int i = 0; i < recordActivities.Count - 1; i++)
                    {
                        var currentItem = recordActivities[i];
                        if (allowedActivities.Any(x => x == currentItem.ActivityId))
                        {
                            var nextItem = recordActivities[i + 1];

                            // Nastavíme EndTime aktuální položky na StartTime následující položky
                            currentItem.EndTime = nextItem.StartTime;
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}