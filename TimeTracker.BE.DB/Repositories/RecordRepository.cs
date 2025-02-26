using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;

namespace TimeTracker.BE.DB.Repositories;

public class RecordRepository
{
	private readonly MainDatacontext _context;
	public RecordRepository(MainDatacontext context)
	{
		_context = context;
	}
	/// <summary>
	/// Odstraní záznam aktivity podle zadaného Guid.
	/// </summary>
	/// <param name="guidId">Guid záznamu aktivity.</param>
	/// <returns>Vrací true, pokud byl záznam úspěšně odstraněn.</returns>
	public async Task<bool?> DeleteRecordAsync(Guid guidId)
	{
		try
		{
			using (var context = _context)
			{
				var selectRow = await context.RecordActivities.FirstOrDefaultAsync(x => x.GuidId == guidId);
				if (selectRow != null)
				{
					context.RecordActivities.Remove(selectRow);
					await context.SaveChangesAsync();
				}
			}

			await UpdateRefreshEndTimeAsync();

			return true;
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// Získá poslední záznam aktivity z databáze.
	/// </summary>
	/// <returns>Poslední záznam aktivity.</returns>
	public async Task<RecordActivity?> GetLastRecordActivityAsync()
	{
		try
		{
			RecordActivity? recordActivity = null;

			using (var context = _context)
			{
				if (context.RecordActivities.Count() > 0)
				{
					recordActivity = await context.RecordActivities.OrderBy(x => x.StartDateTime)
						.Include(x => x.Project)
							.ThenInclude(x => x.SubModules)
						.Include(x => x.Activity)
						.Include(x => x.Shift)
						.Include(x => x.SubModule)
						.Include(x => x.TypeShift)
						.LastAsync();
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
	public async Task<RecordActivity?> GetRecordAsync(Guid guidId)
	{
		try
		{
			using (var context = _context)
			{
				var recordActivitiy = await context.RecordActivities
				.Include(x => x.Project)
					.ThenInclude(x => x.SubModules)
				.Include(x => x.Activity)
				.Include(x => x.TypeShift)
				.Include(x => x.Shift)
				.OrderBy(x => x.StartDateTime).FirstOrDefaultAsync(x => x.GuidId == guidId);

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
	public async Task<List<RecordActivity>> GetRecordsAsync()
	{
		try
		{
			using (var context = _context)
			{
				var recordActivities = await context.RecordActivities
				.Include(x => x.Project)
					.ThenInclude(x => x.SubModules)
				.Include(x => x.Activity)
				.Include(x => x.TypeShift)
				.Include(x => x.Shift)
				.OrderBy(x => x.StartDateTime).ToListAsync();

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
	public async Task<List<RecordActivity>> GetRecordsAsync(DateTime startTime, DateTime endTime)
	{
		try
		{
			using (var context = _context)
			{
				var recordActivities = await context.RecordActivities.Where(x => x.StartDateTime >= startTime && x.StartDateTime <= endTime)
				.Include(x => x.Project)
					.ThenInclude(x => x.SubModules)
				.Include(x => x.Activity)
				.Include(x => x.TypeShift)
				.Include(x => x.Shift)
				.OrderBy(x => x.StartDateTime).ToListAsync();

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
	public async Task<RecordActivity?> SaveRecordAsync(RecordActivity recordActivity)
	{
		try
		{
			using (var context = _context)
			{
				if (recordActivity.GuidId != Guid.Empty)
				{
					context.RecordActivities.Update(recordActivity);
				}
				else
				{
					await context.RecordActivities.AddAsync(recordActivity);
				}

				await context.SaveChangesAsync();
			}

			await UpdateRefreshEndTimeAsync();

			var getDat = await GetRecordAsync(recordActivity.GuidId);
			return getDat;
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// Aktualizuje čas ukončení záznamů aktivit.
	/// Pro každý záznam aktivity nastaví čas ukončení na čas zahájení následující aktivity,
	/// pokud se nejedná o poslední záznam nebo aktivitu typu Stop.
	/// </summary>
	public async Task UpdateRefreshEndTimeAsync()
	{
		try
		{
			using (var context = _context)
			{
				var recordActivities = await context.RecordActivities.OrderBy(x => x.StartDateTime).ToListAsync();

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

				await context.SaveChangesAsync();
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
}