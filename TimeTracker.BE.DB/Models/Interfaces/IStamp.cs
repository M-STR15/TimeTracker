namespace TimeTracker.BE.DB.Models.Interfaces
{
	/// <summary>
	/// Rozhraní pro razítko s datem a časem.
	/// </summary>
	public interface IStamp
	{
		/// <summary>
		/// Datum a čas razítka.
		/// </summary>
		DateTime StampDateTime { get; set; }
	}
}