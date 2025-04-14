using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro směnu.
	/// </summary>
	public interface IShift : IShiftBase
	{
		/// <summary>
		/// Dlouhý řetězec s datem a časem začátku směny.
		/// </summary>
		string StartDateLongStr { get; }
		/// <summary>
		/// Kolekce směn.
		/// </summary>
		ICollection<Shift>? Shifts { get; set; }

		/// <summary>
		/// Typ směny.
		/// </summary>
		TypeShift? TypeShift { get; set; }
	}
}