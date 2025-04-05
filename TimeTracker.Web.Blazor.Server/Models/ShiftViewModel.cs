using TimerTracker.BE.Web.BusinessLogic.Models.DTOs;

namespace TimeTracker.Web.Blazor.Server.Models
{
	public class ShiftViewModel : ShiftBaseDto
	{
		public TypeShiftBaseDto? TypeShift { get; set; }

		public ShiftViewModel(Guid guidId, DateTime startDate, int typeShiftId, TypeShiftBaseDto? typeShift, string? description = null) : base(guidId, startDate, typeShiftId, description)
		{
			TypeShift = typeShift;
		}
	}
}
