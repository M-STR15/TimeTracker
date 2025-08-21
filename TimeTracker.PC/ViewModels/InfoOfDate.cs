using TimeTracker.Basic.Enums;

namespace TimeTracker.PC.ViewModels
{
	internal class InfoOfDate
	{
		private string? _description;

		private bool _isPlanShiftInDay;
		/// <summary>
		/// Třída uchovávající informace o konkrétním datu.
		/// </summary>
		/// <param name="date">Datum, ke kterému se informace vztahují.</param>
		/// <param name="guidID">Jedinečný identifikátor záznamu.</param>
		/// <param name="isPlanShiftInDay">Určuje, zda je v daný den plánována směna.</param>
		/// <param name="eTypeShift">Typ směny přiřazený k datu.</param>
		/// <param name="description">Volitelný popis dne nebo směny.</param>
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

		/// <summary>
		/// Datum, ke kterému se informace vztahují.
		/// </summary>
		public DateTime Date { get; private set; }
		/// <summary>
		/// Den v měsíci, ke kterému se informace vztahují.
		/// </summary>
		public int Day { get; private set; }
		/// <summary>
		/// Den v týdnu, ke kterému se informace vztahují.
		/// </summary>
		public DayOfWeek DayOfWeek { get; private set; }

		/// <summary>
		/// Popis dne nebo směny. Nastavením této vlastnosti se označí záznam jako upravený.
		/// </summary>
		public string? Description
		{
			get => _description;
			set
			{
				_description = value;
				IsEdited = true;
			}
		}

		/// <summary>
		/// Jedinečný identifikátor záznamu.
		/// </summary>
		public Guid GuidId { get; private set; }
		/// <summary>
		/// Určuje, zda byl záznam upraven od svého vytvoření nebo posledního uložení.
		/// </summary>
		public bool IsEdited { get; private set; }

		/// <summary>
		/// Určuje, zda je v daný den plánována směna.
		/// Nastavením této vlastnosti se označí záznam jako upravený.
		/// </summary>
		public bool IsPlanShiftInDay
		{
			get => _isPlanShiftInDay;
			set
			{
				_isPlanShiftInDay = value;
				IsEdited = true;
			}
		}

		/// <summary>
		/// Pořadí týdne v měsíci, ve kterém se dané datum nachází.
		/// </summary>
		public int WeekInMont { get; private set; }

		/// <summary>
		/// Typ směny přiřazený k tomuto datu.
		/// </summary>
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