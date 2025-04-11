namespace TimeTracker.Web.Blazor.Server.Models
{
	public class Notification
	{
		public string Title { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;

		public string TimeSinceStr
		{
			get
			{
				var difference = DateTime.Now - _createdAt;

				if (difference.Days > 0)
					return createText(difference.Minutes, "d");

				if (difference.Hours > 0)
					return createText(difference.Minutes, "hod");

				if (difference.Minutes > 0)
					return createText(difference.Minutes, "min");

				return createText(difference.Minutes, "s");
			}
		}

		private string createText(int time, string unit)
		{
			return "před " + time.ToString() + " " + unit;
		}

		public bool IsEnableDeleteTime { get; set; }
		public int DeleteTimeForSeconds { get; set; }

		public eNotificationType Type { get; set; } = eNotificationType.Info;
		private DateTime _createdAt { get; set; } = DateTime.Now;
	}
}