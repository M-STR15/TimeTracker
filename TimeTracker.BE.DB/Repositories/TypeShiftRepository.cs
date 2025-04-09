using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories.Interfaces;

namespace TimeTracker.BE.DB.Repositories
{
	public class TypeShiftRepository<T>(Func<T> contextFactory) : aRepository<T>(contextFactory), IReadtableAll<TypeShift> where T : MainDatacontext
	{
		/// <summary>
		/// Získá všechny typy směn z databáze.
		/// </summary>
		public async Task<IEnumerable<TypeShift>?> GetAllAsync()
		{
			try
			{
				var context = _contextFactory();
				var typeShifts = await context.TypeShifts.AsNoTracking().ToListAsync();
				return typeShifts;
			}
			catch (Exception)
			{
				return default;
			}
		}

		/// <summary>
		/// Získá všechny typy směn, které jsou viditelné v hlavním okně.
		/// </summary>
		public async Task<IEnumerable<TypeShift>?> GetTypeShiftsForMainWindowAsync()
		{
			try
			{
				var context = _contextFactory();
				var typeShifts = await context.TypeShifts.Where(x => x.IsVisibleInMainWindow).AsNoTracking().ToListAsync();
				return typeShifts;
			}
			catch (Exception)
			{
				return default;
			}
		}

	}
}
