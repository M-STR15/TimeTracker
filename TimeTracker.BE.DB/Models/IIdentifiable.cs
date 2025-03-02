namespace TimeTracker.BE.DB.Models
{
	/// <summary>
	/// Rozhraní pro identifikovatelné objekty s int.
	/// </summary>
	public interface IIdentifiable
	{
		/// <summary>
		/// Identifikátor objektu.
		/// </summary>
		int Id { get; }
	}

	/// <summary>
	/// Rozhraní pro identifikovatelné objekty s GUID.
	/// </summary>
	public interface IIdentifiableGuid
	{
		/// <summary>
		/// Identifikátor objektu.
		/// </summary>
		Guid GuidId { get; }
	}
}