using TimeTracker.BE.DB.Models.Enums;

namespace TimeTracker.BE.DB.Repositories.Models.Reports
{
	public class SumInDay
	{
		public DateTime Date { get; set; }
		public string WeekDay { get; private set; }
		public double WorkHours { get; set; }
		public double Pause { get; set; }
		//public double WorkHours_HomeOffice { get; set; }
		//public double Pause_HomeOffice { get; set; }
		//public double WorkHours_Others { get; set; }
		//public double Pause_Others { get; set; }

		public eTypeShift TypeShift { get; set; }

		public SumInDay(DateTime date, eTypeShift typeShift, double workHours = 0, double pause = 0)
		{
			Date = date;
			WeekDay = date.ToString("ddd");
			WorkHours = workHours;
			Pause = pause;
			TypeShift = typeShift;
			//WorkHours_HomeOffice = workHours_Office;
			//Pause_HomeOffice = pause_Office;
			//WorkHours_Others = workHours_Office;
			//Pause_Others = pause_Office;
		}
	}
}