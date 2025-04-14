using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro typ směny.
	/// </summary>
	public interface ITypeShift : ITypeShiftBase
	{
		/// <summary>
		/// Kolekce aktivit záznamů.
		/// </summary>
		ICollection<RecordActivity>? RecordActivity { get; set; }

		/// <summary>
		/// Kolekce typů směn.
		/// </summary>
		ICollection<TypeShift>? TypeShifts { get; set; }
	}
}