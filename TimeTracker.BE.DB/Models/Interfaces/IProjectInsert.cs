namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní vlastnosti projektu.
	/// </summary>
	public interface IProjectInsert
	{
		/// <summary>
		/// Popis projektu.
		/// </summary>
		string? Description { get; set; }

		/// <summary>
		/// Název projektu.
		/// </summary>
		string Name { get; set; }
	}
}