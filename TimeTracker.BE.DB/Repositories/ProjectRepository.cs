namespace TimeTracker.BE.DB.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories.Interfaces;

public class ProjectRepository<T>(Func<T> contextFactory) : aRepository<T>(contextFactory), IWritable<IProjectBase>, IDeletableById, IReadtableAll<Project> where T : MainDatacontext
{
	#region GET

	/// <summary>
	/// Získá všechny projekty z databáze, seřazené podle názvu a včetně jejich podmodulů.
	/// </summary>
	/// <returns>Kolekce projektů.</returns>
	public async Task<IEnumerable<Project>> GetAllAsync()
	{
		try
		{
			var context = _contextFactory();
			return await context.Projects.OrderBy(x => x.Name)
					.Include(x => x.SubModules).ToListAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// Získá projekt z databáze na základě jeho ID.
	/// </summary>
	/// <param name="id">ID projektu, který má být získán.</param>
	/// <returns>Projekt, pokud existuje, jinak null.</returns>
	public async Task<Project?> GetAsync(int id)
	{
		try
		{
			if (id > 0)
			{
				var context = _contextFactory();
				var project = await context.Projects.Where(x => x.Id == id).FirstAsync();
				return project;
			}
			else
			{
				return null;
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion

	/// <summary>
	/// Odstraní projekt z databáze na základě jeho ID.
	/// </summary>
	/// <param name="id">ID projektu, který má být odstraněn.</param>
	/// <returns>True, pokud byl projekt úspěšně odstraněn, jinak false.</returns>
	public async Task<bool> DeleteAsync(int id)
	{
		try
		{
			var result = false;
			var context = _contextFactory();
			var item = await context.Projects.FirstOrDefaultAsync(x => x.Id == id);
			if (item != null)
			{
				context.Projects.Remove(item);
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
	/// Uloží projekt do databáze. Pokud projekt neexistuje, přidá ho, jinak ho aktualizuje.
	/// </summary>
	/// <param name="project">Projekt k uložení.</param>
	/// <returns>Uložený projekt nebo null, pokud projekt s daným názvem již existuje.</returns>
	public async Task<IProjectBase?> SaveAsync(IProjectBase item)
	{
		try
		{
			//var item = project is Project p ? p : throw new InvalidCastException("Nepodařilo se převést na Project");
			var itemCon = new Project(item);
			var context = _contextFactory();
			var existRecord = await context.Projects.AnyAsync(x => x.Name == item.Name);
			if (!existRecord)
			{
				if (item.Id == 0)
					await context.Projects.AddAsync(itemCon);
				else
					context.Projects.Update(itemCon);

				await context.SaveChangesAsync();
			}
			else
			{
				return null;
			}

			return itemCon;
		}
		catch (Exception)
		{
			throw;
		}
	}
}