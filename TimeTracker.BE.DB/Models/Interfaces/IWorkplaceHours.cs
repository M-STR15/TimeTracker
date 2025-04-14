using TimeTracker.BE.DB.Repositories.Models.Reports;

namespace TimeTracker.BE.DB.Models.Interfaces
{
	public interface IWorkplaceHours
	{
		/// <summary>
		/// Seznam pracovních hodin strávených na home office.
		/// </summary>
		IEnumerable<DayHours>? HomeOfficeWorkHourslist { get; set; }
		/// <summary>
		/// Seznam pracovních hodin strávených v kanceláři.
		/// </summary>
		IEnumerable<DayHours>? OfficeWorkHourslist { get; set; }
		/// <summary>
		/// Plánovaný seznam pracovních hodin na home office.
		/// </summary>
		IEnumerable<DayHours>? PlanHomeOfficeWorkHoursList { get; set; }
		/// <summary>
		/// Plánovaný seznam pracovních hodin v kanceláři.
		/// </summary>
		IEnumerable<DayHours>? PlanWorkHoursList { get; set; }
	}
}