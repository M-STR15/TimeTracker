using Microsoft.EntityFrameworkCore;
using TimerTracker.BE.DB.Models;
using TimerTracker.BE.DB.DataAccess;

namespace TimerTracker.BE.DB.Providers
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

		public bool SaveRecord(RecordActivity recordActivity)
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					context.RecordActivities.Add(recordActivity);
					context.SaveChanges();
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
