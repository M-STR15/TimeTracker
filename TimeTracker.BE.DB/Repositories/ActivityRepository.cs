using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Repositories.Interfaces;

namespace TimeTracker.BE.DB.Repositories;

public class ActivityRepository<T>(Func<T> contextFactory) : aRepository<T>(contextFactory), IReadtableAll<Activity> where T : MainDatacontext
{
	/// <summary>
	/// Získá seznam všech aktivit z databáze.
	/// </summary>
	/// <returns>Seznam aktivit.</returns>
	public async Task<IEnumerable<Activity>> GetAllAsync()
	{
		try
		{
			var context = _contextFactory();
			return await context.Activities.ToListAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}
}