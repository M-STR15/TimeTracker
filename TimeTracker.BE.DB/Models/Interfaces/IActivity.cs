namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface IActivity : IActivityBase
	{
		/// <summary>
		/// Kolekce aktivit.
		/// </summary>
		ICollection<RecordActivity>? Activities { get; set; }
	}
}