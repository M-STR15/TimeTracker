namespace TimeTracker.BE.DB.Models
{
	public interface IRecordActivity
	{
		/// <summary>
		/// Aktivita spojená se záznamem.
		/// </summary>
		Activity? Activity { get; set; }

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
		/// Projekt spojený se záznamem.
		/// </summary>
		Project? Project { get; set; }

		/// <summary>
		/// Primární klíč projektu.
		/// </summary>
		int? ProjectId { get; set; }

		/// <summary>
		/// Směna spojená se záznamem.
		/// </summary>
		Shift? Shift { get; set; }

		/// <summary>
		/// Globálně unikátní identifikátor směny.
		/// </summary>
		Guid? ShiftGuidId { get; set; }

		/// <summary>
		/// Datum a čas zahájení aktivity.
		/// </summary>
		DateTime StartDateTime { get; set; }

		/// <summary>
		/// Podmodul spojený se záznamem.
		/// </summary>
		SubModule? SubModule { get; set; }

		/// <summary>
		/// Primární klíč podmodulu.
		/// </summary>
		int? SubModuleId { get; set; }

		/// <summary>
		/// Typ směny spojený se záznamem.
		/// </summary>
		TypeShift? TypeShift { get; set; }

		/// <summary>
		/// Primární klíč typu směny.
		/// </summary>
		int? TypeShiftId { get; set; }
	}
}