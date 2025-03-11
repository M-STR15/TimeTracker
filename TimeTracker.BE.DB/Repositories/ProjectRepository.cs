namespace TimeTracker.BE.DB.Repositories;

using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

public class ProjectRepository<T> where T : MainDatacontext
{
	private readonly Func<T> _contextFactory;
	public ProjectRepository(Func<T> contextFactory)
	{
		_contextFactory = contextFactory;
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
	/// Získá všechny podmoduly z databáze, seřazené podle názvu.
	/// </summary>
	/// <returns>Kolekce podmodulů.</returns>
	public async Task<ICollection<SubModule>> GetSubModulesAsync()
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
	/// <param name="ptojectId">ID projektu</param>
	/// <returns>Kolekce podmodulů.</returns>
	public async Task<ICollection<SubModule>?> GetSubModulesAsync(int ptojectId)
	{
		try
		{
			var context = _contextFactory();
			if (context.SubModules.Any(x => x.ProjectId == ptojectId))
				return await context.SubModules.Where(x => x.ProjectId == ptojectId).OrderBy(x => x.Name).ToListAsync();
			else
				return null;
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