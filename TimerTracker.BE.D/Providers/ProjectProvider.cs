namespace TimerTracker.BE.DB.Providers;
using TimerTracker.BE.D.Models;
using TimerTracker.BE.DB.DataAccess;
public class ProjectProvider
{

	public List<Project> GetProjects()
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				return context.Projects.ToList();
			}
		}
		catch (Exception)
		{
			return new();
		}
	}


	public List<SubModule> GetSubModules()
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				return context.SubModules.ToList();
			}
		}
		catch (Exception)
		{
			return new();
		}
	}

	public List<SubModule> GetSubModules(int ptojectId)
	{
		try
		{
			using (var context = new MainDatacontext())
			{
				if (context.SubModules.Any(x => x.ProjectId == ptojectId))
					return context.SubModules.Where(x => x.ProjectId == ptojectId).ToList();
				else
					return new List<SubModule>();
			}
		}
		catch (Exception)
		{
			return new();
		}
	}

	public bool SaveProject(IProjectWithoutColl project)
	{
		try
		{
			var item = new Project(project);

			using (var context = new MainDatacontext())
			{
				if (item.Id == 0)
					context.Projects.Add(item);
				else
					context.Projects.Update(item);

				context.SaveChanges();
			}

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool SaveSubModule(ISubModuleWithoutColl subModule)
	{
		try
		{
			var item = new SubModule(subModule);

			using (var context = new MainDatacontext())
			{
				if (item.Id == 0)
					context.SubModules.Add(item);
				else
					context.SubModules.Update(item);

				context.SaveChanges();
			}

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
