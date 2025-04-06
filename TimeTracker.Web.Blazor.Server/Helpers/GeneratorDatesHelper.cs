using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.Helpers
{
	internal static class GeneratorDatesHelper
	{
		internal static IEnumerable<MonthAndYearItemViewModel> GetMontData()
		{
			List<MonthAndYearItemViewModel> monthAndYears = new();
			var startDate = new DateTime(2025, 1, 1);
			var endDate = DateTime.Today;

			while (startDate <= endDate)
			{
				monthAndYears.Add(new MonthAndYearItemViewModel(startDate));
				startDate = startDate.AddMonths(1);
			}

			return monthAndYears.OrderByDescending(x => x.Date);
		}

		internal static IEnumerable<ShiftViewModel> GetDaysInMonth(MonthAndYearItemViewModel item, List<ShiftBaseDto> planShiftsInDB, List<TypeShiftBaseDto> typeShifts)
		{
			List<ShiftViewModel> _days = new();
			var daysInMonth = DateTime.DaysInMonth(item.Year, item.Month);
			for (int i = 0; i < daysInMonth; i++)
			{
				var startDate = new DateTime(item.Year, item.Month, i + 1);
				var shiftInDb = planShiftsInDB.FirstOrDefault(x => x.StartDate == startDate);
				var existShift = (shiftInDb != null);

				Guid guidId = existShift ? shiftInDb.GuidId : Guid.Empty;
				int typeShiftId = existShift ? shiftInDb.TypeShiftId : 0;
				string? description = existShift ? shiftInDb?.Description : null;
				var typeShift = typeShifts.FirstOrDefault(x => x.Id == typeShiftId);
				_days.Add(new ShiftViewModel(guidId, startDate, typeShiftId, typeShift, description));
			}

			return _days;
		}
	}
}
