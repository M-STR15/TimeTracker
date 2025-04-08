using System.Text.Json.Serialization;

namespace TimeTracker.BE.Web.BusinessLogic.Models.DTOs
{
	public class RecordActivityDetailDto : RecordActivityBaseDto
	{
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual ActivityBaseDto? Activity { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual ProjectBaseDto? Project { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual ShiftBaseDto? Shift { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual SubModuleBaseDto? SubModule { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual TypeShiftBaseDto? TypeShift { get; set; }
	}
}
