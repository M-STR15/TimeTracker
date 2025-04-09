using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ShiftBaseDto : ShiftInsertDto, IShiftBase
	{
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		public Guid GuidId { get; set; }

		public ShiftBaseDto()
		{ }

		public ShiftBaseDto(Guid guidId, DateTime startDate, int typeShiftId, string? description = null)
		{
			Description = description;
			GuidId = guidId;
			StartDate = startDate;
			TypeShiftId = typeShiftId;
		}
	}
}
