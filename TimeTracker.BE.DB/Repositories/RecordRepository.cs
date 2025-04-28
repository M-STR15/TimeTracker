using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Repositories.Interfaces;
using TimeTracker.Basic.Enums;
using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.BE.DB.Repositories;

public class RecordRepository<T>(Func<T> contextFactory) : aRepository<T>(contextFactory), IWritable<RecordActivity>, IDeletableByGuidId, IReadtableAll<RecordActivity> where T : MainDatacontext
{

	#region DELETE
	/// <summary>
	/// Odstraní záznam aktivity podle zadaného Guid.
	/// </summary>
	/// <param name="guidId">Guid záznamu aktivity.</param>
	/// <returns>Vrací true, pokud byl záznam úspěšně odstraněn.</returns>
	public async Task<bool> DeleteAsync(Guid guidId)
	{
		try
		{
			var result = false;
			var context = _contextFactory();
			var selectRow = await context.RecordActivities.FirstOrDefaultAsync(x => x.GuidId == guidId);
			if (selectRow != null)
			{
				context.RecordActivities.Remove(selectRow);
				await context.SaveChangesAsync();
				result = true;
			}


			await updateRefreshEndTimeAsync();

			return result;
		}
		catch (Exception)
		{
			throw;
		}
	}

	#endregion DELETE
	#region GET
	/// <summary>
	/// Získá všechny záznamy aktivit z databáze.
	/// </summary>
	/// <returns>Seznam všech záznamů aktivit.</returns>
	public async Task<IEnumerable<RecordActivity>> GetAllAsync()
	{
		try
		{
			var context = _contextFactory();
			var recordActivities = await context.RecordActivities
				.Include(x => x.Project)
					.ThenInclude(x => x.SubModules)
				.Include(x => x.Activity)
				.Include(x => x.TypeShift)
				.Include(x => x.Shift)
				.OrderBy(x => x.StartDateTime).ToListAsync();

			return recordActivities;
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
	public async Task<RecordActivity?> GetAsync(Guid guidId)
	{
		try
		{
			var context = _contextFactory();
			var recordActivitiy = await context.RecordActivities
				.Include(x => x.Project)
					.ThenInclude(x => x.SubModules)
				.Include(x => x.Activity)
				.Include(x => x.TypeShift)
				.Include(x => x.Shift)
				.OrderBy(x => x.StartDateTime).FirstOrDefaultAsync(x => x.GuidId == guidId);

			return recordActivitiy;
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
	public async Task<IEnumerable<RecordActivity>> GetAsync(DateTime startTime, DateTime endTime)
	{
		try
		{
			var context = _contextFactory();
			var recordActivities = await context.RecordActivities.Where(x => x.StartDateTime >= startTime && x.StartDateTime <= endTime)
				.Include(x => x.Project)
					.ThenInclude(x => x.SubModules)
				.Include(x => x.Activity)
				.Include(x => x.TypeShift)
				.Include(x => x.Shift)
				.OrderBy(x => x.StartDateTime).ToListAsync();

			return recordActivities;
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
	public async Task<RecordActivity?> GetLastAsync()
	{
		try
		{
			RecordActivity? recordActivity = null;

			var context = _contextFactory();
			if (context.RecordActivities.Count() > 0)
			{
				recordActivity = await context.RecordActivities.OrderByDescending(x => x.StartDateTime)
					.Include(x => x.Project)
						.ThenInclude(x => x.SubModules)
					.Include(x => x.Activity)
					.Include(x => x.Shift)
					.Include(x => x.SubModule)
					.Include(x => x.TypeShift)
					.FirstOrDefaultAsync();
			}

			return recordActivity;
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion GET
	#region SAVE
	/// <summary>
	/// Uloží záznam aktivity do databáze.
	/// Pokud záznam obsahuje Guid, aktualizuje existující záznam, jinak přidá nový.
	/// </summary>
	/// <param name="recordActivity">Záznam aktivity k uložení.</param>
	/// <returns>Uložený záznam aktivity.</returns>
	public async Task<RecordActivity?> SaveAsync(RecordActivity item)
	{
		try
		{
			var context = _contextFactory();
			if (item.GuidId != Guid.Empty)
				context.RecordActivities.Update(item);
			else
				await context.RecordActivities.AddAsync(item);

			await context.SaveChangesAsync();

			await updateRefreshEndTimeAsync();

			var getDat = item;
			return getDat;
		}
		catch (Exception)
		{
			throw;
		}
	}

	private static List<int> _allowedActivities = new()
			{
				(int)eActivity.Start,
				(int)eActivity.Pause
			};

	//TODO dodělat, aby se při běžném zápisu měnila hodnota pouze u posledního zápisu před tímto, z důvodu zefektivnění
	/// <summary>
	/// Aktualizuje čas ukončení záznamů aktivit.
	/// Pro každý záznam aktivity nastaví čas ukončení na čas zahájení následující aktivity,
	/// pokud se nejedná o poslední záznam nebo aktivitu typu Stop.
	/// </summary>
	private async Task updateRefreshEndTimeAsync()
	{
		try
		{
			var context = _contextFactory();
			var recordActivities = await context.RecordActivities.OrderBy(x => x.StartDateTime).ToListAsync();

			for (int i = 0; i <= recordActivities.Count - 1; i++)
			{
				var currentItem = recordActivities[i];
				//u podledního zápisu se nemění endTime
				if (i == recordActivities.Count - 1)
				{
					currentItem.EndDateTime = null;
				}
				// pokdu existuje následující aktivita připíše datum dané aktivity jako konec aktivity před ní
				else if (_allowedActivities.Any(x => x == currentItem.ActivityId))
				{
					var nextItem = recordActivities[i + 1];
					currentItem.EndDateTime = nextItem.StartDateTime;
				}
			}

			await context.SaveChangesAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion SAVE
}