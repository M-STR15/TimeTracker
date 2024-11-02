using TimerTracker.BE.DB.DataAccess;
using TimerTracker.BE.DB.Models;
using TimerTracker.BE.DB.Models.Enums;

namespace TimerTracker.BE.DB.Providers
{
    public class ReportProvider
    {

        public List<SumInDay> GetActivityOverDays(DateTime dayFrom, DateTime dayTo)
        {
            var activities = new List<RecordActivity>();

            try
            {
                using (var context = new MainDatacontext())
                {
                    var basicAllData = context.RecordActivities
                            .Where(x => x.StartTime >= dayFrom && x.StartTime <= dayTo).OrderBy(x => x.StartTime).ToList();

                    var basicAllData2 = basicAllData
                        .Zip(basicAllData.Skip(1), (current, next) => new RecordSum(
                            activityId: current.ActivityId,
                            day: current.StartTime.Date,
                            dateTimeFrom: current.StartTime,
                            dateTimeTo: next.StartTime,// Nastavíme StartTime z následujícího záznamu jako DatetimeTo
                            typeShiftId: current.TypeShiftId
                        ))
                        .ToList();


                    var list = new List<SumInDay>();
                    list = getDatesInRange(dayFrom, dayTo).Select(x => new SumInDay(x)).ToList();
                    foreach (var item in list)
                    {
                        item.WorkHours_Office = getSumHours(basicAllData2, item.Date, eTypeShift.Office, eActivity.Start);
                        item.Pause_Office = getSumHours(basicAllData2, item.Date, eTypeShift.Office, eActivity.Pause);

                        item.WorkHours_HomeOffice = getSumHours(basicAllData2, item.Date, eTypeShift.HomeOffice, eActivity.Start);
                        item.Pause_HomeOffice = getSumHours(basicAllData2, item.Date, eTypeShift.HomeOffice, eActivity.Pause);

                        item.WorkHours_Others = getSumHours(basicAllData2, item.Date, eTypeShift.Others, eActivity.Start);
                        item.Pause_Others = getSumHours(basicAllData2, item.Date, eTypeShift.Others, eActivity.Pause);
                    }

                    return list;
                }
            }
            catch (Exception)
            {
                return new List<SumInDay>();
            }
        }

        private static List<DateTime> getDatesInRange(DateTime start, DateTime end)
        {
            return Enumerable.Range(0, (end - start).Days)
                             .Select(offset => start.AddDays(offset))
                             .ToList();
        }

        private double getSumHours(List<RecordSum> list, DateTime date, eTypeShift typeShiftFilter, eActivity activityFilter)
        {
            return Math.Round(list.Where(x => x.Day == date && x.TypeShiftId == (int)typeShiftFilter && x.ActivityId == (int)activityFilter).Sum(x => x.DurationSec) / 3600, 2);
            ;
        }
    }

    public class SumInDay
    {
        public DateTime Date { get; set; }
        public string WeekDay { get; private set; }
        public double WorkHours_Office { get; set; }
        public double Pause_Office { get; set; }
        public double WorkHours_HomeOffice { get; set; }
        public double Pause_HomeOffice { get; set; }
        public double WorkHours_Others { get; set; }
        public double Pause_Others { get; set; }

        public SumInDay(DateTime date, double workHours_Office = 0, double pause_Office = 0, double workHours_HomeOffice = 0, double pause_HomeOffice = 0, double workHours_Others = 0, double pause_Others = 0)
        {
            Date = date;
            WeekDay = date.ToString("ddd");
            WorkHours_Office = workHours_Office;
            Pause_Office = pause_Office;
            WorkHours_HomeOffice = workHours_Office;
            Pause_HomeOffice = pause_Office;
            WorkHours_Others = workHours_Office;
            Pause_Others = pause_Office;
        }
    }

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
