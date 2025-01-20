using Microsoft.EntityFrameworkCore;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;

namespace TimeTracker.BE.DB.Providers
{
	public class ShiftProvider
	{
		public ShiftProvider()
		{ }

		/// <summary>
		/// Získá všechny směny z databáze, seřazené podle data začátku.
		/// </summary>
		public List<Shift> GetShifts()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var shifts = context.Shifts.OrderBy(x => x.StartDate).AsNoTracking().ToList();
					return shifts;
				}
			}
			catch (Exception)
			{
				return new();
			}
		}

		/// <summary>
		/// Získá všechny směny v zadaném časovém rozmezí.
		/// </summary>
		/// <param name="dateFrom">Počáteční datum.</param>
		/// <param name="dateTo">Koncové datum.</param>
		/// <returns>Seznam směn v zadaném časovém rozmezí.</returns>
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

		/// <summary>
		/// Získá všechny typy směn z databáze.
		/// </summary>
		public List<TypeShift> GetTypeShifts()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var typeShifts = context.TypeShifts.AsNoTracking().ToList();
					return typeShifts;
				}
			}
			catch (Exception)
			{
				return new();
			}
		}

		/// <summary>
		/// Získá všechny typy směn, které jsou viditelné v hlavním okně.
		/// </summary>
		public List<TypeShift> GetTypeShiftsForMainWindow()
		{
			try
			{
				using (var context = new MainDatacontext())
				{
					var typeShifts = context.TypeShifts.Where(x => x.IsVisibleInMainWindow).AsNoTracking().ToList();
					return typeShifts;
				}
			}
			catch (Exception)
			{
				return new();
			}
		}

		/// <summary>
		/// Uloží seznam směn do databáze. Aktualizuje existující směny, přidá nové a odstraní ty, které již nejsou v seznamu.
		/// </summary>
		/// <param name="shifts">Seznam směn k uložení.</param>
		/// <returns>Vrací true, pokud operace proběhla úspěšně, jinak false.</returns>
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
				throw new ArgumentException("Chyba při ukládání směny do DB.", ex);
			}

		}
		/// <summary>
		/// METHODA NENÍ DOKONČENÁ
		/// </summary>
		/// <param name="shiftGuidId"></param>
		/// <returns></returns>
		public double GetSumShiftHours(Guid shiftGuidId)
		{
			using (var context = new MainDatacontext())
			{

			}

			return 0;
		}
	}
}