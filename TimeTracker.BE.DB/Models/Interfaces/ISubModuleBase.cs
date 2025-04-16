namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní podmodul.
	/// </summary>
	public interface ISubModuleBase : ISubModuleInsert
	{
		/// <summary>
		/// Identifikátor podmodulu.
		/// </summary>
		int Id { get; }
	}
}