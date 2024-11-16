namespace TimeTracker.BE.DB.Providers;

using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

public class ProjectProvider
{
	public ICollection<Project> GetProjects()
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				return context.Projects.OrderBy(x => x.Name).ToList();
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

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