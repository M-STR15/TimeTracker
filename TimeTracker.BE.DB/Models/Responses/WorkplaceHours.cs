using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.BE.DB.Repositories.Models.Reports;

namespace TimeTracker.BE.DB.Models.Responses
{
	/// <summary>
	/// Třída reprezentující pracovní hodiny na různých pracovištích.
	/// </summary>
	public class WorkplaceHours : IWorkplaceHours
	{
		/// <inheritdoc />
		public IEnumerable<DayHours>? OfficeWorkHourslist { get; set; }

		/// <inheritdoc />
		public IEnumerable<DayHours>? HomeOfficeWorkHourslist { get; set; }

		/// <inheritdoc />
		public IEnumerable<DayHours>? PlanHomeOfficeWorkHoursList { get; set; }

		/// <inheritdoc />
		public IEnumerable<DayHours>? PlanWorkHoursList { get; set; }
	}
}
