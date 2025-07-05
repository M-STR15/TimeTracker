using System;
using System.Reflection;
namespace TimeTracker.Web.Blazor.Server
{
	public class BuildInfo
	{
		public string BranchName => "develop";
		public DateTime BuildDate => DateTime.Parse("2025-07-05 22:22:53");
		public string VersionStr => getVersion();

		private string getVersion()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version;
			return version == null ? "unknown" : $"{version.Major}.{version.Minor}.{version.Build}";
		}
	}
}
