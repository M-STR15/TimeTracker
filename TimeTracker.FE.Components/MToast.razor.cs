using Microsoft.AspNetCore.Components;
using TimeTracker.FE.Components.Services;

namespace TimeTracker.FE.Components
{
	public partial class MToast
	{
		private Timer _timerRemove;

		[Inject]
		private ToastNotificationService _notificationService { get; set; }

		[Parameter]
		public Notification Notification { get; set; } = default!;

		//[Parameter]
		//public string Title { get; set; } = string.Empty;

		//[Parameter]
		//public DateTime CreatedAt { get; set; } = DateTime.Now;

		//[Parameter]
		//public string Message { get; set; } = string.Empty;
		//[Parameter]
		//public bool IsEnableDeleteTime { get; set; }
		//[Parameter]
		//public int DeleteTimeFor { get; set; }

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			if (Notification.IsEnableDeleteTime)
			{
				_timerRemove = new Timer(removeNotification, null, Notification.DeleteTimeForSeconds * 1000, 1);
			}
		}

		private void deleteNotification()
		{
			_notificationService.RemoveNotification(Notification);
		}

		private void removeNotification(object state)
		{
			_notificationService.RemoveNotification(Notification);
		}
	}
}