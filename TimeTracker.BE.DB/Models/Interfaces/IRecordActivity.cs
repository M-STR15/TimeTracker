namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface IRecordActivity : IRecordActivityBase
	{
		/// <summary>
		/// Aktivita spojená se záznamem.
		/// </summary>
		Activity? Activity { get; set; }

		/// <summary>
		/// Projekt spojený se záznamem.
		/// </summary>
		Project? Project { get; set; }

		/// <summary>
		/// Směna spojená se záznamem.
		/// </summary>
		Shift? Shift { get; set; }

		/// <summary>
		/// Podmodul spojený se záznamem.
		/// </summary>
		SubModule? SubModule { get; set; }

		/// <summary>
		/// Typ směny spojený se záznamem.
		/// </summary>
		TypeShift? TypeShift { get; set; }
	}
}