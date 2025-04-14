namespace TimeTracker.BE.DB.Models
{
	/// <summary>
	/// Třída pro výpočet hodin, obsahuje pracovní a pauzové hodiny.
	/// </summary>
	public class CalcHours : ICalcHours
	{
		/// <inheritdoc />
		public double WorkHours { get; set; }

		/// <inheritdoc />
		public double PauseHours { get; set; }
	}
}
