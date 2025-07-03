using TimeTracker.FE.Web.Components.Models;

namespace TimeTracker.FE.Web.Components
{
	public class Notification
	{
		public Guid GuidId { get; private set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; private set; } = DateTime.Now;

		public int? DeleteTimeForSeconds { get; private set; }

		public bool IsEnableDeleteTime { get; set; }

		public string Message { get; set; } = string.Empty;

		public string TimeSinceStr
		{
			get
			{
				var difference = DateTime.Now - CreatedAt;

				if (difference.Days > 0)
					return createText(difference.Days, "d");

				if (difference.Hours > 0)
					return createText(difference.Hours, "hod");

				if (difference.Minutes > 0)
					return createText(difference.Minutes, "min");

				return createText(difference.Seconds, "s");
			}
		}

		public string Title { get; set; } = string.Empty;

		public eNotificationType Type { get; set; } = eNotificationType.Info;

		public Notification(int deleteTimeForSeconds)
		{
			DeleteTimeForSeconds = deleteTimeForSeconds;
		}
		private string createText(int time, string unit)
		{
			if (unit == "s")
				return "před méně než minutou";
			else
				return "před " + time.ToString() + " " + unit;
		}

		public DateTime? GetExitTime()
		{
			if (IsEnableDeleteTime)
				return CreatedAt.AddSeconds(DeleteTimeForSeconds ?? 0);
			else
				return null;
		}
	}
}