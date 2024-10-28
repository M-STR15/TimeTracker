using Microsoft.EntityFrameworkCore;
using TimerTracker.BE.DB.DataAccess;
using TimerTracker.BE.DB.Models;

namespace TimerTracker.BE.DB.Providers
{
    public class ShiftProvider
    {
        public ShiftProvider()
        { }

        public List<Shift> GetShifts()
        {
            try
            {
                using (var context = new MainDatacontext())
                {
                    var shifts = context.Shifts.AsNoTracking().ToList();
                    return shifts;
                }
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
                using (var context = new MainDatacontext())
                {

                    var shifts = context.Shifts.Where(x => x.StartDate >= dateFrom && x.StartDate.Date <= dateTo).AsNoTracking().ToList();
                    return shifts;
                }
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
                using (var context = new MainDatacontext())
                {
                    if (shifts == null || !shifts.Any())
                        return true;

                    var firstDate = shifts.First().StartDate;
                    var year = firstDate.Year;
                    var month = firstDate.Month;
                    var from = new DateTime(year, month, 1);
                    var to = from.AddMonths(1);
                    var actualDataInDB = context.Shifts
                        .Where(x => x.StartDate >= from && x.StartDate < to)
                        .AsNoTracking()
                        .ToList();

                    var updateList = shifts.Where(x => x.GuidId != Guid.Empty).ToList();
                    var addList = shifts.Where(x => x.GuidId == Guid.Empty).ToList();
                    var dellList = actualDataInDB
                        .ExceptBy(shifts.Select(e => e.GuidId), x => x.GuidId)
                        .ToList();

                    if (dellList != null && dellList.Count > 0)
                    {
                        context.Shifts.RemoveRange(dellList);
                        context.SaveChanges();
                    }

                    if (updateList != null && updateList.Count > 0)
                        context.Shifts.UpdateRange(updateList);

                    if (addList != null && addList.Count > 0)
                        context.Shifts.AddRange(addList);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Zde můžeš přidat logování chyby pro další diagnostiku
                Console.WriteLine(ex.Message); // Například log do konzole
                return false;
            }
        }
    }
}
