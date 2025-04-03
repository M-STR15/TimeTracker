using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(Shift))]
	public class ShiftBaseDto : IShiftBase
	{
		/// <inheritdoc />
		[JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]
		public string? Description { get; set; }
		/// <inheritdoc />
		public Guid GuidId { get; set; }
		/// <inheritdoc />
		public DateTime StartDate { get; set; }
		/// <inheritdoc />
		public int TypeShiftId { get; set; }
	}
}
