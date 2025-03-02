using TimeTracker.BE.Web.Shared.Models;

namespace TimeTracker.Web.Blazor.Server.Components
{
	public partial class NavMenu
	{
		BuildInfo _buildInfo = new BuildInfo();
		private bool collapseNavMenu = true;

		private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private void ToggleNavMenu()
		{
			collapseNavMenu = !collapseNavMenu;
		}

		private void onOpenDocumentPageClick() => NavigationManager.NavigateTo("/swagger/index.html", true);

		private void onOpenEventLogsPageClick() => NavigationManager.NavigateTo("/event-logs");
	}
}
