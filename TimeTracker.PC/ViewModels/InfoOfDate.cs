using TimeTracker.Enums;

namespace TimeTracker.PC.ViewModels
{
	internal class InfoOfDate
	{
		private string? _description;

		private bool _isPlanShiftInDay;

		public InfoOfDate(DateTime date, Guid guidID, bool isPlanShiftInDay, eTypeShift eTypeShift, string? description = null)
		{
			GuidId = guidID;
			Date = date;
			Day = date.Day;
			DayOfWeek = date.DayOfWeek;
			WeekInMont = getWeekInMonth(date);
			Description = description;
			IsPlanShiftInDay = isPlanShiftInDay;
			ETypeShift = eTypeShift;
			IsEdited = false;
		}

		public DateTime Date { get; private set; }
		public int Day { get; private set; }
		public DayOfWeek DayOfWeek { get; private set; }

		public string? Description
		{
			get => _description;
			set
			{
				_description = value;
				IsEdited = true;
			}
		}

		public Guid GuidId { get; private set; }
		public bool IsEdited { get; private set; }

		public bool IsPlanShiftInDay
		{
			get => _isPlanShiftInDay;
			set
			{
				_isPlanShiftInDay = value;
				IsEdited = true;
			}
		}

		public int WeekInMont { get; private set; }

		public eTypeShift ETypeShift { get; set; }

		/// <summary>
		/// Vrátí sloupec odpovídající danému dni v týdnu.
		/// </summary>
		/// <param name="dayOfWeek">Den v týdnu.</param>
		/// <returns>Číslo sloupce, kde neděle je 6 a ostatní dny jsou posunuty o -1.</returns>
		public static int GetColumn(DayOfWeek dayOfWeek) => dayOfWeek == DayOfWeek.Sunday ? 6 : (int)dayOfWeek - 1;

		private static int getWeekInMonth(DateTime date)
		{
			DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
			int firstDayOfWeek = GetColumn(firstDayOfMonth.DayOfWeek); // 0 = neděle, 1 = pondělí, ...
			int daysSinceStartOfMonth = date.Day;
			int totalDaysBeforeCurrentWeek = daysSinceStartOfMonth + firstDayOfWeek - 1;
			int weekNumber = totalDaysBeforeCurrentWeek / 7 + 1;

			return weekNumber;
		}
	}
}