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
				return context.SubModules.Where(x => x.ProjectId == ptojectId).ToList();
			}
		}
		catch (Exception)
		{
			return new();
		}
	}
}
