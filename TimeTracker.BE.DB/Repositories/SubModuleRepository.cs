using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories.Interfaces;

namespace TimeTracker.BE.DB.Repositories
{
	public class SubModuleRepository<T>(Func<T> contextFactory) : aRepository<T>(contextFactory), IWritable<ISubModuleBase>, IDeletableById,  IReadtableAll<SubModule> where T : MainDatacontext
	{

		#region GET
		/// <summary>
		/// Získá všechny podmoduly z databáze, seřazené podle názvu.
		/// </summary>
		/// <returns>Kolekce podmodulů.</returns>
		public async Task<IEnumerable<SubModule>> GetAllAsync()
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
		public async Task<IEnumerable<SubModule>?> GetForTheProjectAsync(int projectId)
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
		#endregion GET

		/// <summary>  
		/// Odstraní podmodul z databáze podle jeho ID.  
		/// </summary>  
		/// <param name="id">ID podmodulu, který má být odstraněn.</param>  
		/// <returns>True, pokud byl podmodul úspěšně odstraněn, jinak false.</returns>  
		public async Task<bool> DeleteAsync(int id)
		{
			try
			{
				var result = false;
				var context = _contextFactory();
				var item = await context.SubModules.FirstOrDefaultAsync(x => x.Id == id);
				if (item != null)
				{
					context.SubModules.Remove(item);
					await context.SaveChangesAsync();
					result = true;
				}

				return result;
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
		public async Task<ISubModuleBase?> SaveAsync(ISubModuleBase item)
		{
			try
			{
				var itemCon = new SubModule(item);

				var context = _contextFactory();
				if (item.Id == 0)
					await context.SubModules.AddAsync(itemCon);
				else
					context.SubModules.Update(itemCon);

				await context.SaveChangesAsync();

				return itemCon;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
