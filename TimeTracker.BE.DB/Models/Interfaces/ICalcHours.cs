namespace TimeTracker.BE.DB.Models
{
	public interface ICalcHours
	{
		/// <summary>
		/// Počet odpracovaných hodin.
		/// </summary>
		double PauseHours { get; set; }
		/// <summary>
		/// Počet hodin strávených na pauzách.
		/// </summary>
		double WorkHours { get; set; }
	}
}