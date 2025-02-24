using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.BE.DB.Repositories;
public class ActivityRepository
{
	/// <summary>
	/// Získá seznam všech aktivit z databáze.
	/// </summary>
	/// <returns>Seznam aktivit.</returns>
	public async Task<List<Activity>> GetActivitiesAsync()
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				return await context.Activities.ToListAsync();
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
}