using TimeTracker.BE.Web.Shared.Models;

namespace TimeTracker.Web.Blazor.Server.Components
{
	public partial class NavMenu
	{
		private BuildInfo _buildInfo = new();
		private bool collapseNavMenu = true;

		private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private void ToggleNavMenu()
		{
			collapseNavMenu = !collapseNavMenu;
		}

		private void onOpenDocumentPage_Click() => NavigationManager.NavigateTo("/swagger/index.html", true);
		private void onOpenCurrentActivityPage_Click() => NavigationManager.NavigateTo("/current-activity");
		private void onOpenSettings_Click() => NavigationManager.NavigateTo("/settings");
		private void onOpenShifts_Click() => NavigationManager.NavigateTo("/shifts");
		private void onOpenReportRecordList_Click() => NavigationManager.NavigateTo("/reports/record-list");
		private void onOpenReportActivitiesOverDays_Click() => NavigationManager.NavigateTo("/reports/activities-over-days");
		private void onOpenReportPlanVsRealityWorkHours_Click() => NavigationManager.NavigateTo("/reports/plan-vs-reality-work-hours");

	}
}
