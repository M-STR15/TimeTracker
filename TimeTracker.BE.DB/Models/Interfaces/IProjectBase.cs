namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní vlastnosti projektu.
	/// </summary>
	public interface IProjectBase
	{
		/// <summary>
		/// Popis projektu.
		/// </summary>
		string? Description { get; set; }

		/// <summary>
		/// Identifikátor projektu.
		/// </summary>
		int Id { get; set; }

		/// <summary>
		/// Název projektu.
		/// </summary>
		string Name { get; set; }
	}
}