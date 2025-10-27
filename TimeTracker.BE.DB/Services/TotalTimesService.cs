using TimeTracker.Basic.Enums;
using TimeTracker.BE.DB.Models.Responses;
using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.PC.Services
{
	public class TotalTimesService
	{
		/// <summary>
		/// Methoda pro získání celkových časů podle jednotlivých kategorií
		/// </summary>
		/// <param name="calcHours"></param>
		/// <param name="calcHoursShift"></param>
		/// <param name="_lastRecordActivity"></param>
		/// <returns></returns>
		public static TotalTimes Get(CalcHours calcHours, CalcHours calcHoursShift, RecordActivity? _lastRecordActivity)
		{
			var totalTimes = new TotalTimes();
			var filterToday = DateTime.Now;
			var actualTime = GetActualTime(filterToday, _lastRecordActivity).TotalSeconds;

			var selShift = _lastRecordActivity?.Shift;
			if (_lastRecordActivity != null)
			{
				var activity = (eActivity)_lastRecordActivity?.ActivityId;

				var workHours_actual = ((activity == eActivity.Start) ? actualTime : 0);
				var pauseHours_actual = ((activity == eActivity.Pause) ? actualTime : 0);

				var workShiftHours_actual = getWorkTimeShift(selShift, activity, actualTime);
				var pauseShifteHours_actual = getPauseTimeShift(selShift, activity, actualTime);

				var workShiftHours_fromDB = calcHoursShift.WorkHours;
				var pauseShiftHours_fromDB = calcHoursShift.PauseHours;

				totalTimes.ActivityId = _lastRecordActivity.ActivityId;
				totalTimes.ShiftGuidId = _lastRecordActivity.ShiftGuidId;
				totalTimes.ActualTime = GetActualTime(filterToday, _lastRecordActivity);

				totalTimes.WorkTime = TimeSpan.FromSeconds(calcHours.WorkHours + workHours_actual);
				totalTimes.PauseTime = TimeSpan.FromSeconds(calcHours.PauseHours + pauseHours_actual);
				totalTimes.TotalTime = TimeSpan.FromSeconds(calcHours.WorkHours + calcHours.PauseHours + workHours_actual + pauseHours_actual);

				totalTimes.WorkShiftTime = TimeSpan.FromSeconds(workShiftHours_fromDB + workShiftHours_actual);
				totalTimes.PauseShiftTime = TimeSpan.FromSeconds(pauseShiftHours_fromDB + pauseShifteHours_actual);
				totalTimes.TotalShiftTime = TimeSpan.FromSeconds(workShiftHours_fromDB + pauseShiftHours_fromDB + workShiftHours_actual + pauseShifteHours_actual);
			}

			return totalTimes;
		}

		public static TimeSpan GetActualTime(DateTime filterToday, RecordActivity? _lastRecordActivity)
		{
			if (_lastRecordActivity != null && _lastRecordActivity.ActivityId != (int)eActivity.Stop)
				return (filterToday - _lastRecordActivity.StartDateTime);
			else
				return new TimeSpan();
		}

		private static double getWorkTimeShift(Shift? shift, eActivity activity, double actualActivityInSeconds)
		{
			return (isShift(shift) && activity == eActivity.Start) ? actualActivityInSeconds : 0;
		}

		private static double getPauseTimeShift(Shift? shift, eActivity activity, double actualActivityInSeconds)
		{
			return (isShift(shift) && activity == eActivity.Pause) ? actualActivityInSeconds : 0;
		}

		private static bool isShift(Shift? shift) => (shift != null && shift.GuidId != Guid.Empty);
	}
}
