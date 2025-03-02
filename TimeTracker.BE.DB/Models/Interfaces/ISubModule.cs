namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro podmodul.
	/// </summary>
	public interface ISubModule : ISubModuleBase
	{
		/// <summary>
		/// Kolekce aktivit spojených s podmodulem.
		/// </summary>
		ICollection<RecordActivity>? Activities { get; set; }

		/// <summary>
		/// Projekt, ke kterému podmodul patří.
		/// </summary>
		Project? Project { get; set; }
	}
}