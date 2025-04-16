namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní informace o směně.
	/// </summary>
	public interface IShiftBase
	{
		/// <summary>
		/// Popis směny.
		/// </summary>
		string? Description { get; set; }
		/// <summary>
		/// Identifikátor směny.
		/// </summary>
		Guid GuidId { get; }
		/// <summary>
		/// Datum a čas začátku směny.
		/// </summary>
		DateTime StartDate { get; set; }
		/// <summary>
		/// Identifikátor typu směny.
		/// </summary>
		int TypeShiftId { get; set; }
	}
}