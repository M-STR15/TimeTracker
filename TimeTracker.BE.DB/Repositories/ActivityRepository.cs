using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.BE.DB.Repositories;

public class ActivityRepository<T> : aRepository<T> where T : MainDatacontext
{
	public ActivityRepository(Func<T> contextFactory) : base(contextFactory)
	{}
	/// <summary>
	/// Získá seznam všech aktivit z databáze.
	/// </summary>
	/// <returns>Seznam aktivit.</returns>
	public async Task<List<Activity>> GetActivitiesAsync()
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