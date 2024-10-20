using TimerTracker.BE.D.Models;

namespace TimerTracker.BE.DB.Providers;
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

}
