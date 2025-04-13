using TimeTracker.BE.DB.Repositories.Models.Reports;

namespace TimeTracker.BE.DB.Models
{
	/// <summary>
	/// Třída reprezentující pracovní hodiny na různých pracovištích.
	/// </summary>
	public class WorkplaceHours
	{
		/// <summary>
		/// Seznam pracovních hodin strávených v kanceláři.
		/// </summary>
		public IEnumerable<DayHours>? OfficeWorkHourslist { get; set; }

		/// <summary>
		/// Seznam pracovních hodin strávených na home office.
		/// </summary>
		public IEnumerable<DayHours>? HomeOfficeWorkHourslist { get; set; }

		/// <summary>
		/// Plánovaný seznam pracovních hodin na home office.
		/// </summary>
		public IEnumerable<DayHours>? PlanHomeOfficeWorkHoursList { get; set; }

		/// <summary>
		/// Plánovaný seznam pracovních hodin v kanceláři.
		/// </summary>
		public IEnumerable<DayHours>? PlanWorkHoursList { get; set; }
	}
}
