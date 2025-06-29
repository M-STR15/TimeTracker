namespace TimeTracker.FE.Web.Components
{
	public partial class MToastComponent
	{
		protected override void OnInitialized()
		{
			_notificationService.OnChange += OnNotificationServiceChange;
		}

		private void OnNotificationServiceChange()
		{
			InvokeAsync(StateHasChanged);
		}

		public void Dispose()
		{
			_notificationService.OnChange -= OnNotificationServiceChange;
		}
	}
}