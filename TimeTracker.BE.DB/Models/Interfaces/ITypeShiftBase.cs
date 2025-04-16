namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní typ směny.
	/// </summary>
	public interface ITypeShiftBase
	{
		/// <summary>
		/// Barva směny.
		/// </summary>
		string Color { get; set; }

		/// <summary>
		/// Identifikátor směny.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// Viditelnost směny v hlavním okně.
		/// </summary>
		bool IsVisibleInMainWindow { get; set; }

		/// <summary>
		/// Název směny.
		/// </summary>
		string Name { get; set; }
	}
}