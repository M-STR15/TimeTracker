using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class RecordActivityBaseDto : IRecordActivityBase
	{
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována.")]
		public int ActivityId { get; set; }
		/// <inheritdoc />
		public string? Description { get; set; }
		/// <inheritdoc />
		public DateTime? EndDateTime { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována.")]
		public Guid GuidId { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována.")]
		public int? ProjectId { get; set; }
		/// <inheritdoc />
		public Guid? ShiftGuidId { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována.")]
		public DateTime StartDateTime { get; set; }
		/// <inheritdoc />
		public int? SubModuleId { get; set; }
		/// <inheritdoc />
		public int? TypeShiftId { get; set; }
	}
}
