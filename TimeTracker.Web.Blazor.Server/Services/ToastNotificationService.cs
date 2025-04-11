using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.Services
{
	public class ToastNotificationService
	{
		public event Action OnChange;

		private List<Notification> _notifications = new();

		public IEnumerable<Notification> Notifications => _notifications;

		public void AddNotification(eNotificationType type, string title, string message, bool isEnableDeleteTime = false, int deleteTimeForSeconds = 5)
		{
			var notification = new Notification
			{
				Message = message,
				Title = title,
				Type = type,
				IsEnableDeleteTime = isEnableDeleteTime,
				DeleteTimeForSeconds = deleteTimeForSeconds
			};

			_notifications.Add(notification);
			NotifyStateChanged();
		}

		public void RemoveNotification(Notification message)
		{
			_notifications.Remove(message);
			NotifyStateChanged();
		}

		private void NotifyStateChanged()
		{
			if (OnChange != null)
			{
				OnChange.Invoke();
			}
		}
	}
}
