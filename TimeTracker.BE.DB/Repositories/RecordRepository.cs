using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;

namespace TimeTracker.BE.DB.Repositories
{
	public class RecordRepository
	{
		/// <summary>
		/// Odstraní záznam aktivity podle zadaného Guid.
		/// </summary>
		/// <param name="guidId">Guid záznamu aktivity.</param>
		/// <returns>Vrací true, pokud byl záznam úspěšně odstraněn.</returns>
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

		/// <summary>
		/// Získá poslední záznam aktivity z databáze.
		/// </summary>
		/// <returns>Poslední záznam aktivity.</returns>
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

		/// <summary>
		/// Získá záznam aktivity podle zadaného Guid.
		/// </summary>
		/// <param name="guidId">Guid záznamu aktivity.</param>
		/// <returns>Záznam aktivity.</returns>
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

		/// <summary>
		/// Získá všechny záznamy aktivit z databáze.
		/// </summary>
		/// <returns>Seznam všech záznamů aktivit.</returns>
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
		/// <summary>
		/// Získá záznamy aktivit v zadaném časovém rozmezí.
		/// </summary>
		/// <param name="startTime">Počáteční časový bod.</param>
		/// <param name="endTime">Koncový časový bod.</param>
		/// <returns>Seznam záznamů aktivit v zadaném časovém rozmezí.</returns>
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
		/// <summary>
		/// Uloží záznam aktivity do databáze.
		/// Pokud záznam obsahuje Guid, aktualizuje existující záznam, jinak přidá nový.
		/// </summary>
		/// <param name="recordActivity">Záznam aktivity k uložení.</param>
		/// <returns>Uložený záznam aktivity.</returns>
		public RecordActivity? SaveRecord(RecordActivity recordActivity)
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					if (recordActivity.GuidId != Guid.Empty)
					{
						context.RecordActivities.Update(recordActivity);
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
		/// <summary>
		/// Aktualizuje čas ukončení záznamů aktivit.
		/// Pro každý záznam aktivity nastaví čas ukončení na čas zahájení následující aktivity,
		/// pokud se nejedná o poslední záznam nebo aktivitu typu Stop.
		/// </summary>
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
						if (i == recordActivities.Count - 1)
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