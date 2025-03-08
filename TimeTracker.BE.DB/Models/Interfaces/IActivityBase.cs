namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface IActivityBase
	{
		/// <summary>
		/// Primární klíč.
		/// </summary>
		int Id { get; set; }

		/// <summary>
		/// Název aktivity.
		/// </summary>
		string Name { get; set; }
	}
}