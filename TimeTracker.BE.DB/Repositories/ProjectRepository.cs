namespace TimeTracker.BE.DB.Repositories;

using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

public class ProjectRepository<T> : aRepository<T> where T : MainDatacontext
{
	public ProjectRepository(Func<T> contextFactory):base(contextFactory) 
	{
	
	}
	/// <summary>
	/// Odstraní projekt z databáze.
	/// </summary>
	/// <param name="project">Projekt k odstranění.</param>
	/// <returns>Odstraněný projekt nebo null, pokud projekt neexistuje.</returns>
	public async Task<IProjectBase?> DeleteProjectAsync(IProjectBase project)
	{
		try
		{
			var context = _contextFactory();
			var item = await context.Projects.FirstOrDefaultAsync(x => x.Id == project.Id);
			if (item != null)
			{
				context.Projects.Remove(item);
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

	public async Task<bool> DeleteProjectAsync(int projectId)
	{
		try
		{
			var context = _contextFactory();
			var item = await context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
			if (item != null)
			{
				context.Projects.Remove(item);
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
	/// Získá všechny projekty z databáze, seřazené podle názvu a včetně jejich podmodulů.
	/// </summary>
	/// <returns>Kolekce projektů.</returns>
	public async Task<ICollection<Project>> GetProjectsAsync()
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
	/// Uloží projekt do databáze. Pokud projekt neexistuje, přidá ho, jinak ho aktualizuje.
	/// </summary>
	/// <param name="project">Projekt k uložení.</param>
	/// <returns>Uložený projekt nebo null, pokud projekt s daným názvem již existuje.</returns>
	public async Task<IProjectBase?> SaveProjectAsync(IProjectBase project)
	{
		try
		{
			//var item = project is Project p ? p : throw new InvalidCastException("Nepodařilo se převést na Project");
			var item = new Project(project);
			var context = _contextFactory();
			var existRecord = await context.Projects.AnyAsync(x => x.Name == item.Name);
			if (!existRecord)
			{
				if (item.Id == 0)
					await context.Projects.AddAsync(item);
				else
					context.Projects.Update(item);

				await context.SaveChangesAsync();
			}
			else
			{
				return null;
			}

			return item;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}