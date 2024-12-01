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
						.ThenInclude(x => x.SubModules)
					.Include(x => x.Activity)
					.Include(x => x.TypeShift)
					.Include(x => x.Shift)
					.OrderBy(x => x.StartDateTime).ToList();

					return recordActivities;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public RecordActivity GetRecord(Guid guidId)
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var recordActivitiy = context.RecordActivities
					.Include(x => x.Project)
						.ThenInclude(x => x.SubModules)
					.Include(x => x.Activity)
					.Include(x => x.TypeShift)
					.Include(x => x.Shift)
					.OrderBy(x => x.StartDateTime).FirstOrDefault(x => x.GuidId == guidId);

					return recordActivitiy;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<RecordActivity> GetRecords(DateTime startTime, DateTime endTime)
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var recordActivities = context.RecordActivities.Where(x => x.StartDateTime >= startTime && x.StartDateTime <= endTime)
					.Include(x => x.Project)
						.ThenInclude(x => x.SubModules)
					.Include(x => x.Activity)
					.Include(x => x.TypeShift)
					.Include(x => x.Shift)
					.OrderBy(x => x.StartDateTime).ToList();

					return recordActivities;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public RecordActivity GetLastRecordActivity()
		{
			try
			{
				RecordActivity? recordActivity = null;

				using (var context = new MainDatacontext())
				{
					if (context.RecordActivities.Count() > 0)
					{
						recordActivity = context.RecordActivities.OrderBy(x => x.StartDateTime)
							.Include(x => x.Project)
								.ThenInclude(x => x.SubModules)
							.Include(x => x.Activity)
							.Include(x => x.Shift)
							.Include(x => x.SubModule)
							.Include(x => x.TypeShift)
							.Last();
					}
				}

				return recordActivity;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public RecordActivity? SaveRecord(RecordActivity recordActivity)
		{
			try
			{
				//var findValue = recordActivity;
				using (var context = new MainDatacontext())
				{
					if (recordActivity.GuidId != Guid.Empty)
					{
						//findValue = context.RecordActivities.FirstOrDefault(x => x.GuidId == recordActivity.GuidId);
						//if (findValue != null)
						//{
						//findValue.SetBasicValues(recordActivity);
						context.RecordActivities.Update(recordActivity);
						//}
					}
					else
					{
						context.RecordActivities.Add(recordActivity);
					}

					context.SaveChanges();
				}

				UpdateRefreshEndTime();

				var getDat = GetRecord(recordActivity.GuidId);
				return getDat;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public bool DeleteRecord(Guid guidId)
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var selectRow = context.RecordActivities.FirstOrDefault(x => x.GuidId == guidId);
					context.RecordActivities.Remove(selectRow);
					context.SaveChanges();
				}

				UpdateRefreshEndTime();

				return true;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public void UpdateRefreshEndTime()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var recordActivities = context.RecordActivities.OrderBy(x => x.StartDateTime).ToList();

					var allowedActivities = new List<int>();
					allowedActivities.Add((int)eActivity.Start);
					allowedActivities.Add((int)eActivity.Pause);

					for (int i = 0; i <= recordActivities.Count - 1; i++)
					{
						var currentItem = recordActivities[i];
						if (i == (recordActivities.Count - 1))
						{
							currentItem.EndDateTime = null;
						}
						else if (currentItem.ActivityId != (int)eActivity.Stop && allowedActivities.Any(x => x == currentItem.ActivityId))
						{
							var nextItem = recordActivities[i + 1];
							currentItem.EndDateTime = nextItem.StartDateTime;
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