using TimerTracker.BE.DB.DataAccess;
using TimerTracker.BE.DB.Models;
using TimerTracker.BE.DB.Models.Enums;
using TimerTracker.BE.DB.Providers.Models.Reports;

namespace TimerTracker.BE.DB.Providers
{
    public class ReportProvider
    {

        public List<SumInDay> GetActivityOverDays(DateTime start, DateTime end)
        {
            var activities = new List<RecordActivity>();

            try
            {
                var modBasicData = getRecordSumList(start, end);
                var list = new List<SumInDay>();
                var dayList = getDatesInRange(start, end);
                foreach (var item in dayList)
                {
                    foreach (eTypeShift typeShift in Enum.GetValues(typeof(eTypeShift)))
                    {
                        var workHours = getSumHours(modBasicData, item.Date, typeShift, eActivity.Start);
                        var pauseHours = getSumHours(modBasicData, item.Date, typeShift, eActivity.Pause);
                        list.Add(new SumInDay(item.Date, typeShift, workHours, pauseHours));
                    }
                }

                return list;
            }
            catch (Exception)
            {
                return new List<SumInDay>();
            }
        }

        private List<RecordSum> getRecordSumList(DateTime start, DateTime end)
        {
            using (var context = new MainDatacontext())
            {
                var basicData = context.RecordActivities
                        .Where(x => x.StartTime >= start && x.StartTime <= end).OrderBy(x => x.StartTime).ToList();

                return basicData
                        .Zip(basicData.Skip(1), (current, next) => new RecordSum(
                            activityId: current.ActivityId,
                            day: current.StartTime.Date,
                            dateTimeFrom: current.StartTime,
                            dateTimeTo: next.StartTime,
                            typeShiftId: current.TypeShiftId)).ToList();
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
        }

        public List<DayHours> GetPlanVsRealitaWorkHours(DateTime start, DateTime end, eTypeShift[] typeShifts)
        {
            var dateList = getDatesInRange(start, end).Select(x => new DayHours(x.Date)).ToList();
            var planList = getPlanList_DayHours(dateList);

            var realData = GetActivityOverDays(start, end);
            return getRealList_DayHours(dateList, realData, typeShifts);
        }

        private List<DayHours> getPlanList_DayHours(List<DayHours> basicList)
        {
            var cumHours = 0.00;
            var planList = new List<DayHours>();
            for (int i = 0; i < basicList.Count; i++)
            {
                var current = basicList[i];
                var newRecord = new DayHours(current, current.IsWorkDay ? 7.5 : 0, cumHours);
                planList.Add(newRecord);
                cumHours = newRecord.CumHours;
            }

            return planList;
        }

        private List<DayHours> getRealList_DayHours(List<DayHours> basicList, List<SumInDay> sumInDayList, eTypeShift[] typeShifts)
        {
            var cumHours = 0.00;
            var planList = new List<DayHours>();
            for (int i = 0; i < basicList.Count; i++)
            {
                var current = basicList[i];
                var workHoursInDay = sumInDayList.Where(x => x.Date == current.Date && typeShifts.Any(y => y == x.TypeShift)).Sum(x => x.WorkHours);

                var newRecord = new DayHours(current, workHoursInDay, cumHours);
                planList.Add(newRecord);
                cumHours = newRecord.CumHours;
            }

            return planList;
        }
    }


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
