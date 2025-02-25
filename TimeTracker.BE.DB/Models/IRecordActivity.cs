namespace TimeTracker.BE.DB.Models
{
	public interface IRecordActivity
	{
		Activity? Activity { get; set; }
		int ActivityId { get; set; }
		string? Description { get; set; }
		double DurationSec { get; }

		//string? EndDate { get; }
		DateTime? EndDateTime { get; set; }

		//string? EndTime { get; }
		Guid GuidId { get; set; }

		Project? Project { get; set; }
		int? ProjectId { get; set; }
		Shift? Shift { get; set; }
		Guid? ShiftGuidId { get; set; }

		//string StartDate { get; }
		DateTime StartDateTime { get; set; }

		//string StartTime { get; }
		SubModule? SubModule { get; set; }

		int? SubModuleId { get; set; }
		TypeShift? TypeShift { get; set; }
		int? TypeShiftId { get; set; }
	}
}