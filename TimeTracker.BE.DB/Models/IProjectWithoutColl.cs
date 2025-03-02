namespace TimeTracker.BE.DB.Models
{
	/// <summary>
	/// Rozhraní pro projekt bez kolekce.
	/// </summary>
	public interface IProjectWithoutColl
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