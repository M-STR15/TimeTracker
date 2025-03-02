namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface IRecordActivityBase
	{
		/// <summary>
		/// Primární klíč aktivity.
		/// </summary>
		int ActivityId { get; set; }

		/// <summary>
		/// Popis aktivity.
		/// </summary>
		string? Description { get; set; }

		/// <summary>
		/// Doba trvání aktivity v sekundách.
		/// </summary>
		double DurationSec { get; }

		/// <summary>
		/// Datum a čas ukončení aktivity.
		/// </summary>
		DateTime? EndDateTime { get; set; }

		/// <summary>
		/// Globálně unikátní identifikátor záznamu.
		/// </summary>
		Guid GuidId { get; set; }

		/// <summary>
		/// Primární klíč projektu.
		/// </summary>
		int? ProjectId { get; set; }

		/// <summary>
		/// Globálně unikátní identifikátor směny.
		/// </summary>
		Guid? ShiftGuidId { get; set; }

		/// <summary>
		/// Datum a čas zahájení aktivity.
		/// </summary>
		DateTime StartDateTime { get; set; }

		/// <summary>
		/// Primární klíč podmodulu.
		/// </summary>
		int? SubModuleId { get; set; }

		/// <summary>
		/// Primární klíč typu směny.
		/// </summary>
		int? TypeShiftId { get; set; }
	}
}