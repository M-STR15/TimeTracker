using TimerTracker.DataAccess;
using TimerTracker.Models;

namespace TimerTracker.Providers
{
	public class ShiftProviders
	{
		private readonly MainDatacontext _mainDatacontext;

		public ShiftProviders(MainDatacontext mainDatacontext)
		{
			_mainDatacontext = mainDatacontext;
		}

		public List<Shift> GetShifts()
		{
			try
			{
				var shifts = _mainDatacontext.Shifts.ToList();
				return shifts;
			}
			catch (Exception)
			{
				return new();
			}
		}

		public List<Shift> GetShifts(DateTime dateFrom, DateTime dateTo)
		{
			try
			{
				var shifts = _mainDatacontext.Shifts.Where(x => x.StartDate >= dateFrom && x.StartDate.Date <= dateTo).ToList();
				return shifts;
			}
			catch (Exception)
			{
				return new();
			}
		}

		public bool SaveShifts(List<Shift> shifts)
		{
			try
			{
				var firstDate = shifts.First().StartDate;
				var year = firstDate.Year;
				var month = firstDate.Month;
				var from = new DateTime(year, month, 1);
				var to = from.AddMonths(1);
				var actualDataInDB = _mainDatacontext.Shifts.Where(x => x.StartDate >= from && x.StartDate < to).ToList();

				var updateList = shifts.Where(x => x.GuidId != Guid.Empty).ToList();
				var addList = shifts.Where(x => x.GuidId == Guid.Empty).ToList();
				var dellList = actualDataInDB.Except(shifts).ToList();

				if (dellList != null && dellList.Count > 0)
					_mainDatacontext.Shifts.RemoveRange(dellList);

				if (updateList != null && updateList.Count > 0)
					_mainDatacontext.Shifts.UpdateRange(updateList);

				if (addList != null && addList.Count > 0)
					_mainDatacontext.Shifts.AddRange(addList);

				_mainDatacontext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
