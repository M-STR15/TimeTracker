using TimeTracker.Basic.Enums;

namespace TimeTracker.BE.DB.Repositories.Models.Reports
{
	public class SumInDay
	{
		public DateTime Date { get; set; }
		public string WeekDay { get; private set; }
		public double WorkHours { get; set; }
		public double Pause { get; set; }

		public eTypeShift TypeShift { get; set; }

		public SumInDay(DateTime date, eTypeShift typeShift, double workHours = 0, double pause = 0)
		{
			Date = date;
			WeekDay = date.ToString("ddd");
			WorkHours = workHours;
			Pause = pause;
			TypeShift = typeShift;
		}
	}
}