using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimeTracker.BE.DB.Models.Interfaces;
using TimeTracker.Basic.Enums;

namespace TimeTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class RecordActivityInsertDto : IRecordActivityInsert
	{
		/// <inheritdoc />
		[Required(ErrorMessage = "Activity:Hodnota je vyžadována")]
		[EnumDataType(typeof(eActivity), ErrorMessage = "Activity:Hodnota neodpovídá požadovanému typu.")]
		public int ActivityId { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Description { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Project:Hodnota je vyžadována")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? ProjectId { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? ShiftGuidId { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "StartDateTime:Hodnota je vyžadována")]
		public DateTime StartDateTime { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? SubModuleId { get; set; }
		/// <inheritdoc />
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? TypeShiftId { get; set; }
	}
}
