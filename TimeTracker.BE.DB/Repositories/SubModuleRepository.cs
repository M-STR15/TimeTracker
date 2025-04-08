using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Repositories
{
	public class SubModuleRepository<T> : aRepository<T> where T : MainDatacontext
	{
		public SubModuleRepository(Func<T> contextFactory) : base(contextFactory)
		{
		}

		/// <summary>
		/// Získá všechny podmoduly z databáze, seřazené podle názvu.
		/// </summary>
		/// <returns>Kolekce podmodulů.</returns>
		public async Task<IEnumerable<SubModule>> GetSubModulesAsync()
		{
			try
			{
				var context = _contextFactory();
				return await context.SubModules.OrderBy(x => x.Name).ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Získá všechny podmoduly z databáze podle ID projektu, seřazené podle názvu.
		/// </summary>
		/// <param name="projectId">ID projektu</param>
		/// <returns>Kolekce podmodulů.</returns>
		public async Task<IEnumerable<SubModule>?> GetSubModulesAsync(int projectId)
		{
			try
			{
				var context = _contextFactory();
				if (context.SubModules.Any(x => x.ProjectId == projectId))
					return await context.SubModules.Where(x => x.ProjectId == projectId).OrderBy(x => x.Name).ToListAsync();
				else
					return null;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Odstraní podmodul z databáze.
		/// </summary>
		/// <param name="subModule">Podmodul k odstranění.</param>
		/// <returns>Odstraněný podmodul nebo null, pokud podmodul neexistuje.</returns>
		public async Task<ISubModuleBase?> DeleteSubModuleAsync(ISubModuleBase subModule)
		{
			try
			{
				var context = _contextFactory();
				var item = await context.SubModules.FirstOrDefaultAsync(x => x.Id == subModule.Id);
				if (item != null)
				{
					context.SubModules.Remove(item);
					await context.SaveChangesAsync();
				}
				else
				{
					return null;
				}
				return item;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> DeleteSubModuleAsync(int subModuleId)
		{
			try
			{
				var context = _contextFactory();
				var item = await context.SubModules.FirstOrDefaultAsync(x => x.Id == subModuleId);
				if (item != null)
				{
					context.SubModules.Remove(item);
					await context.SaveChangesAsync();
				}
				else
				{
					return false;
				}
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Uloží podmodul do databáze. Pokud podmodul neexistuje, přidá ho, jinak ho aktualizuje.
		/// </summary>
		/// <param name="subModule">Podmodul k uložení.</param>
		/// <returns>Uložený podmodul nebo null, pokud došlo k chybě.</returns>
		public async Task<ISubModuleBase?> SaveSubModuleAsync(ISubModuleBase subModule)
		{
			try
			{
				var item = new SubModule(subModule);

				var context = _contextFactory();
				if (item.Id == 0)
					await context.SubModules.AddAsync(item);
				else
					context.SubModules.Update(item);

				await context.SaveChangesAsync();

				return item;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
