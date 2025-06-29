using TimeTracker.FE.Web.Components.Models;

namespace TimeTracker.FE.Web.Components.Services
{
	public class ToastNotificationService : IDisposable
	{
		private Timer _timerRemove;

		/// <summary>
		/// Událost, která je vyvolána při změně seznamu notifikací.
		/// Komponenty mohou tuto událost odebírat pro aktualizaci svého stavu.
		/// </summary>
		public event Action OnChange = default;

		private List<Notification> _notifications = new();


		public ToastNotificationService()
		{
			_timerRemove = new Timer(removeNotificationTick, null, 1000, 1);
		}

		private void removeNotificationTick(object state)
		{
			var removeList = new List<Guid>();
			var now = DateTime.Now;
			foreach (var item in _notifications)
			{
				if (item.GetExitTime() <= now)
				{
					removeList.Add(item.GuidId);
				}
			}

			foreach (var item in removeList)
			{
				RemoveNotification(item);
			}
		}
		/// <summary>
		/// Vrací seznam aktuálních notifikací.
		/// Komponenty mohou tento seznam použít k zobrazení notifikací uživateli.
		/// Seznam je pouze pro čtení.
		/// </summary>
		public IEnumerable<Notification> Notifications => _notifications;


		///<summary>
		/// Přidá novou notifikaci do seznamu a upozorní odběratele na změnu stavu.
		/// </summary>
		/// <param name="type">Typ notifikace.</param>
		/// <param name="title">Nadpis notifikace.</param>
		/// <param name="message">Zpráva notifikace.</param>
		/// <param name="isEnableDeleteTime">Povolit automatické odstranění po čase.</param>
		/// <param name="deleteTimeForSeconds">Doba v sekundách, po které bude notifikace odstraněna.</param>
		public void AddNotification(eNotificationType type, string title, string message, bool isEnableDeleteTime = true, int deleteTimeForSeconds = 5)
		{
			var notification = new Notification(deleteTimeForSeconds)
			{
				Message = message,
				Title = title,
				Type = type,
				IsEnableDeleteTime = isEnableDeleteTime
			};

			_notifications.Add(notification);
			NotifyStateChanged();
		}

		/// <summary>
		/// Odstraní notifikaci ze seznamu a upozorní odběratele na změnu stavu.
		/// </summary>
		/// <param name="message">Notifikace, která má být odstraněna.</param>
		public void RemoveNotification(Notification message)
		{
			_notifications.Remove(message);
			NotifyStateChanged();
		}

		public void RemoveNotification(Guid guidId)
		{
			var notification = _notifications.FirstOrDefault(n => n.GuidId == guidId);
			if (notification != null)
				RemoveNotification(notification);
		}

		private void NotifyStateChanged()
		{
			if (OnChange != null)
			{
				OnChange.Invoke();
			}
		}

		public void Dispose()
		{
			_timerRemove.Dispose();
			_timerRemove = null;
		}
	}
}
