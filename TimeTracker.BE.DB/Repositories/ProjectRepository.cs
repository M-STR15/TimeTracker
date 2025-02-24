namespace TimeTracker.BE.DB.Repositories;

using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

public class ProjectRepository
{
	/// <summary>
	/// Odstraní projekt z databáze.
	/// </summary>
	/// <param name="project">Projekt k odstranění.</param>
	/// <returns>Odstraněný projekt nebo null, pokud projekt neexistuje.</returns>
	public async Task<IProjectWithoutColl?> DeleteProjectAsync(IProjectWithoutColl project)
	{
		try
		{
			using (var context = new MainDatacontext())
			{
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
	public async Task<ISubModuleWithoutColl?> DeleteSubModuleAsync(ISubModuleWithoutColl subModule)
	{
		try
		{
			using (var context = new MainDatacontext())
			{
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
			using (var context = new MainDatacontext())
			{
				return await context.Projects.OrderBy(x => x.Name)
					.Include(x => x.SubModules).ToListAsync();
			}
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
			using (var context = new MainDatacontext())
			{
				return await context.SubModules.OrderBy(x => x.Name).ToListAsync();
			}
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
			using (var context = new MainDatacontext())
			{
				if (context.SubModules.Any(x => x.ProjectId == ptojectId))
					return await context.SubModules.Where(x => x.ProjectId == ptojectId).OrderBy(x => x.Name).ToListAsync();
				else
					return null;
			}
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
	public async Task<IProjectWithoutColl?> SaveProjectAsync(IProjectWithoutColl project)
	{
		try
		{
			var item = new Project(project);

			using (var context = new MainDatacontext())
			{
				if (await context.Projects.AnyAsync(x => x.Name != project.Name))
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
			}

			return item;
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
	public async Task<ISubModuleWithoutColl?> SaveSubModuleAsync(ISubModuleWithoutColl subModule)
	{
		try
		{
			var item = new SubModule(subModule);

			using (var context = new MainDatacontext())
			{
				if (item.Id == 0)
					await context.SubModules.AddAsync(item);
				else
					context.SubModules.Update(item);

				await context.SaveChangesAsync();
			}

			return item;
		}
		catch (Exception)
		{
			throw;
		}
	}
}