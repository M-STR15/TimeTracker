namespace TimeTracker.BE.DB.Providers;

using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

public class ProjectProvider
{
	/// <summary>
	/// Získá všechny projekty z databáze, seřazené podle názvu a včetně jejich podmodulů.
	/// </summary>
	/// <returns>Kolekce projektů.</returns>
	public ICollection<Project> GetProjects()
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				return context.Projects.OrderBy(x => x.Name)
					.Include(x => x.SubModules).ToList();
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
	public ICollection<SubModule> GetSubModules()
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				return context.SubModules.OrderBy(x => x.Name).ToList();
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
	public ICollection<SubModule> GetSubModules(int ptojectId)
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				if (context.SubModules.Any(x => x.ProjectId == ptojectId))
					return context.SubModules.Where(x => x.ProjectId == ptojectId).OrderBy(x => x.Name).ToList();
				else
					return new List<SubModule>();
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
	public IProjectWithoutColl? SaveProject(IProjectWithoutColl project)
	{
		try
		{
			var item = new Project(project);

			using (var context = new MainDatacontext())
			{
				if (context.Projects.Any(x => x.Name != project.Name))
				{
					if (item.Id == 0)
						context.Projects.Add(item);
					else
						context.Projects.Update(item);

					context.SaveChanges();
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
	/// Odstraní projekt z databáze.
	/// </summary>
	/// <param name="project">Projekt k odstranění.</param>
	/// <returns>Odstraněný projekt nebo null, pokud projekt neexistuje.</returns>
	public IProjectWithoutColl DeleteProject(IProjectWithoutColl project)
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				var item = context.Projects.FirstOrDefault(x => x.Id == project.Id);
				if (item != null)
				{
					context.Projects.Remove(item);
					context.SaveChanges();
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
	/// Uloží podmodul do databáze. Pokud podmodul neexistuje, přidá ho, jinak ho aktualizuje.
	/// </summary>
	/// <param name="subModule">Podmodul k uložení.</param>
	/// <returns>Uložený podmodul nebo null, pokud došlo k chybě.</returns>
	public ISubModuleWithoutColl? SaveSubModule(ISubModuleWithoutColl subModule)
	{
		try
		{
			var item = new SubModule(subModule);

			using (var context = new MainDatacontext())
			{
				if (item.Id == 0)
				{
					context.SubModules.Add(item);
				}
				else
				{
					context.SubModules.Update(item);
				}

				context.SaveChanges();
			}

			return item;
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
	public ISubModuleWithoutColl? DeleteSubModule(ISubModuleWithoutColl subModule)
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				var item = context.SubModules.FirstOrDefault(x => x.Id == subModule.Id);
				if (item != null)
				{
					context.SubModules.Remove(item);
					context.SaveChanges();
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
}