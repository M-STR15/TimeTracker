using TimerTracker.BE.Web.BusinessLogic.Models.DTOs;

namespace TimeTracker.Web.Blazor.Server.Models
{
	public class DayItem
	{
		public int Day { get; set; }
		public DateTime Date { get; set; }

		public DayOfWeek DayOfTheWeek { get; set; }

		public TypeShiftBaseDto? TypeShift { get; set; }

		public DayItem(DateTime date)
		{
			Day = date.Day;
			Date = date;
			DayOfTheWeek = date.DayOfWeek;
		}
	}
}
