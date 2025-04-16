namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface IRecordActivityBase : IRecordActivityInsert
	{
		/// <summary>
		/// Datum a čas ukončení aktivity.
		/// </summary>
		DateTime? EndDateTime { get; set; }

		/// <summary>
		/// Globálně unikátní identifikátor záznamu.
		/// </summary>
		Guid GuidId { get; }
	}
}