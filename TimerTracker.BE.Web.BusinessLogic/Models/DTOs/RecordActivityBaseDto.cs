using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class RecordActivityBaseDto : IRecordActivityBase
	{
		/// <inheritdoc />
		public int ActivityId { get; set; }
		/// <inheritdoc />
		public string? Description { get; set; }
		/// <inheritdoc />
		public DateTime? EndDateTime { get; set; }
		/// <inheritdoc />
		public Guid GuidId { get; set; }
		/// <inheritdoc />
		public int? ProjectId { get; set; }
		/// <inheritdoc />
		public Guid? ShiftGuidId { get; set; }
		/// <inheritdoc />
		public DateTime StartDateTime { get; set; }
		/// <inheritdoc />
		public int? SubModuleId { get; set; }
		/// <inheritdoc />
		public int? TypeShiftId { get; set; }
	}
}
