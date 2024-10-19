using TimerTracker.DataAccess;
using TimerTracker.Models.Database;

namespace TimerTracker.Providers
{
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
}
