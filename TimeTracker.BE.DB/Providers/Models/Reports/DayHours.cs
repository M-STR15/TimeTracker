namespace TimeTracker.BE.DB.Providers.Models.Reports
{
    public class DayHours
    {
        public DateTime Date { get; set; }
        public double DateHours { get; set; }
        public double CumHours { get; set; }
        public bool IsWorkDay { get; private set; }
        public DayHours()
        { }
        public DayHours(DateTime date, double dateHours = 0, double cumHours = 0)
        {
            Date = date;
            setIsWorkDay();
            DateHours = dateHours;
            CumHours = cumHours;
        }
        private void setIsWorkDay()
        {
            IsWorkDay = ((int)Date.DayOfWeek >= 1 && (int)Date.DayOfWeek <= 5);
        }

        public DayHours(DayHours dayHours) : this(dayHours.Date, dayHours.DateHours, dayHours.CumHours)
        { }

        public DayHours(DayHours currentDayHours, double dateHours = 0, double cumHours = 0)
        {
            Date = currentDayHours.Date;
            setIsWorkDay();
            DateHours = dateHours;
            CumHours = cumHours + dateHours;
        }
    }
}
