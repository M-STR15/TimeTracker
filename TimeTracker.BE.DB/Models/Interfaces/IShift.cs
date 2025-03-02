namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro směnu.
	/// </summary>
	public interface IShift : IShiftBase
	{
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