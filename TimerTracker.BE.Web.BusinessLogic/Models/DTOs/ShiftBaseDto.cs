using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ShiftBaseDto : IShiftBase
	{
		/// <inheritdoc />
		[JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]
		public string? Description { get; set; }
		/// <inheritdoc />
		[Required]
		public Guid GuidId { get; set; }
		/// <inheritdoc />
		[Required]
		public DateTime StartDate { get; set; }
		/// <inheritdoc />
		[Required]
		public int TypeShiftId { get; set; }
	}
}
