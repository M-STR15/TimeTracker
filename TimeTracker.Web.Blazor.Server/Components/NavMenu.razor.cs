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

		private void onOpenDocumentPage_Click() => NavigationManager.NavigateTo("/swagger/index.html", true);
		private void onOpenEventLogsPage_Click() => NavigationManager.NavigateTo("/event-logs");
		private void onOpenCurrentActivityPage_Click() => NavigationManager.NavigateTo("/current-activity");
		private void onOpenSettings_Click() => NavigationManager.NavigateTo("/settings");
		private void onOpenShifts_Click() => NavigationManager.NavigateTo("/shifts");
		private void onOpenReportRecordList_Click() => NavigationManager.NavigateTo("/reports/record-list");

	}
}
