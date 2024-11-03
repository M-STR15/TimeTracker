namespace TimeTracker.BE.DB.Providers.Models.Reports
{
    internal class RecordSum
    {
        public DateTime Day { get; set; }
        public DateTime DatetimeFrom { get; set; }
        public DateTime DatetimeTo { get; set; }
        public double DurationSec { get => (DatetimeTo - DatetimeFrom).TotalSeconds; }
        public int TypeShiftId { get; set; }
        public int ActivityId { get; set; }

        public RecordSum()
        { }

        public RecordSum(int activityId, DateTime day, DateTime dateTimeFrom, DateTime dateTimeTo, int typeShiftId)
        {
            ActivityId = activityId;
            Day = day;
            DatetimeFrom = dateTimeFrom;
            DatetimeTo = dateTimeTo;
            TypeShiftId = typeShiftId;
        }
    }
}