using Microsoft.AspNetCore.Components;
using TimeTracker.FE.Web.Components.Services;

namespace TimeTracker.FE.Web.Components
{
	public partial class MToast
	{

		[Inject]
		private ToastNotificationService _notificationService { get; set; }

		[Parameter]
		public Notification Notification { get; set; } = default!;

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
		}

		private void deleteNotification()
		{
			_notificationService.RemoveNotification(Notification);
		}
	}
}