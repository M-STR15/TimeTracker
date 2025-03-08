using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ShiftBaseDto : IShiftBase
	{
		/// <inheritdoc />
		public string? Description { get; set; }
		/// <inheritdoc />
		public Guid GuidId { get; set; }
		/// <inheritdoc />
		public DateTime StartDate { get; set; }
		/// <inheritdoc />
		public int TypeShiftId { get; set; }
	}
}
