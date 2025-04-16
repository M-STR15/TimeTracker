namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro základní vlastnosti projektu.
	/// </summary>
	public interface IProjectBase : IProjectInsert
	{
		/// <summary>
		/// Identifikátor projektu.
		/// </summary>
		int Id { get; }
	}
}