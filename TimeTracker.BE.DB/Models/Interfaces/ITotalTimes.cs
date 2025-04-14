namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface ITotalTimes
	{
		/// <summary>
		/// Identifikátor aktivity. Slouží k rozlišení jednotlivých aktivit.
		/// </summary>
		int ActivityId { get; set; }

		/// <summary>
		/// Unikátní identifikátor směny. Může být null, pokud není směna specifikována.
		/// </summary>
		Guid? ShiftGuidId { get; set; }

		/// <summary>
		/// Skutečný čas strávený na aktivitě.
		/// </summary>
		TimeSpan ActualTime { get; set; }

		/// <summary>
		/// Čas strávený prací během aktivity.
		/// </summary>
		TimeSpan WorkTime { get; set; }

		/// <summary>
		/// Čas strávený přestávkami během aktivity.
		/// </summary>
		TimeSpan PauseTime { get; set; }

		/// <summary>
		/// Celkový čas aktivity, který zahrnuje jak pracovní čas, tak přestávky.
		/// </summary>
		TimeSpan TotalTime { get; set; }

		/// <summary>
		/// Pracovní čas během směny.
		/// </summary>
		TimeSpan WorkShiftTime { get; set; }

		/// <summary>
		/// Čas přestávek během směny.
		/// </summary>
		TimeSpan PauseShiftTime { get; set; }

		/// <summary>
		/// Celkový čas směny, který zahrnuje pracovní čas i přestávky.
		/// </summary>
		TimeSpan TotalShiftTime { get; set; }
	}
}