using TimeTracker.BE.DB.Models.Enums;

namespace TimeTracker.Windows.Models
{
    internal class InfoOfDate
    {
        private string? _description;

        private bool _isPlanShiftInDay;

        public InfoOfDate(DateTime date, Guid guidID, bool isPlanShiftInDay, string? description = null)
        {
            GuidId = guidID;
            Date = date;
            Day = date.Day;
            DayOfWeek = date.DayOfWeek;
            WeekInMont = getWeekInMonth(date);
            Description = description;
            IsPlanShiftInDay = isPlanShiftInDay;

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

        public eTypeShift eTypeShift { get; set; }

        public static int GetColumn(DayOfWeek dayOfWeek) => (dayOfWeek == DayOfWeek.Sunday) ? 6 : (int)dayOfWeek - 1;

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
