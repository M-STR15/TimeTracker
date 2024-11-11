using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers.Models.Reports;

namespace TimeTracker.BE.DB.Providers
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

        public double GetPauseHours(DateTime date) => getHours(date, eActivity.Pause);

        public double GetPauseHoursShift(Guid shiftGuidID) => getHoursShif(shiftGuidID, eActivity.Pause);

        public List<DayHours> GetPlanWorkHours(DateTime start, DateTime end, eTypeShift[] typeShifts)
        {
            var dateList = getDatesInRange(start, end).Select(x => new DayHours(x.Date)).ToList();
            return getPlanList_DayHours(start, end, dateList, typeShifts);
        }

        public double GetWorkHours(DateTime date) => getHours(date, eActivity.Start);
        public List<DayHours> GetWorkHours(DateTime start, DateTime end, eTypeShift[] typeShifts)
        {
            var dateList = getDatesInRange(start, end).Select(x => new DayHours(x.Date)).ToList();
            var realData = GetActivityOverDays(start, end);
            return getRealList_DayHours(dateList, realData, typeShifts);
        }

        public double GetWorkHoursShift(Guid shiftGuidID) => getHoursShif(shiftGuidID, eActivity.Start);

        private static List<DateTime> getDatesInRange(DateTime start, DateTime end)
        {
            return Enumerable.Range(0, (end - start).Days)
                             .Select(offset => start.AddDays(offset))
                             .ToList();
        }

        private double getHours(DateTime date, eActivity eActivity)
        {
            var sumHours = 0.00;
            try
            {
                using (var context = new MainDatacontext())
                {
                    foreach (var item in context.RecordActivities.Where(x => x.StartTime >= date.Date && x.StartTime <= date.Date.AddDays(1) && x.ActivityId == (int)eActivity))
                    {
                        sumHours += item.DurationSec;
                    }
                }
            }
            catch (Exception)
            {
            }

            return sumHours;
        }
        private double getHoursShif(Guid shiftGuidID, eActivity eActivity)
        {
            var sumHours = 0.00;
            try
            {
                using (var context = new MainDatacontext())
                {
                    foreach (var item in context.RecordActivities.Where(x => x.ShiftGuidId == shiftGuidID && x.ActivityId == (int)eActivity.Pause))
                    {
                        sumHours += item.DurationSec;
                    }
                }
            }
            catch (Exception)
            {

            }
            return sumHours;
        }
        private List<DayHours> getPlanList_DayHours(DateTime start, DateTime end, List<DayHours> basicList, eTypeShift[] typeShifts)
        {
            var shiftList = new List<Shift>();

            using (var context = new MainDatacontext())
            {
                shiftList = context.Shifts.Where(x => x.StartDate >= start && x.StartDate <= end && typeShifts.Any(y => (int)y == x.TypeShiftId)).ToList();
            }

            var cumHours = 0.00;
            var planList = new List<DayHours>();
            for (int i = 0; i < basicList.Count; i++)
            {
                var current = basicList[i];
                var houtInDay = shiftList.Any(x => x.StartDate == current.Date) ? 7.5 : 0;
                var newRecord = new DayHours(current, houtInDay, cumHours);
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

        private List<RecordActivity> getRecordSumList(DateTime start, DateTime end)
        {
            using (var context = new MainDatacontext())
            {
                var basicData = context.RecordActivities
                        .Where(x => x.StartTime >= start && x.StartTime <= end).OrderBy(x => x.StartTime).ToList();

                return basicData;
            }
        }

        private double getSumHours(List<RecordActivity> list, DateTime date, eTypeShift typeShiftFilter, eActivity activityFilter)
        {
            return Math.Round(list.Where(x => x.StartTime.Date == date && x.TypeShiftId == (int)typeShiftFilter && x.ActivityId == (int)activityFilter).Sum(x => x.DurationSec) / 3600, 2);
        }
    }
}