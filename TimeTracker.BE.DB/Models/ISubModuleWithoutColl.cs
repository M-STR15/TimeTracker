namespace TimeTracker.BE.DB.Models
{
	/// <summary>
	/// Rozhraní pro podmodul bez kolekce
	/// </summary>
	public interface ISubModuleWithoutColl
	{
		/// <summary>
		/// Popis
		/// </summary>
		string? Description { get; set; }
		/// <summary>
		/// Identifikátor
		/// </summary>
		int Id { get; set; }
		/// <summary>
		/// Název
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Identifikátor projektu
		/// </summary>
		int ProjectId { get; set; }
	}
}