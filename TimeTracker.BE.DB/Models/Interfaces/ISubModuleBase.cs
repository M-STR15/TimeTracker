namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní podmodul.
	/// </summary>
	public interface ISubModuleBase
	{
		/// <summary>
		/// Popis podmodulu.
		/// </summary>
		string? Description { get; set; }

		/// <summary>
		/// Identifikátor podmodulu.
		/// </summary>
		int Id { get; set; }

		/// <summary>
		/// Název podmodulu.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Identifikátor projektu, ke kterému podmodul patří.
		/// </summary>
		int ProjectId { get; set; }
	}
}