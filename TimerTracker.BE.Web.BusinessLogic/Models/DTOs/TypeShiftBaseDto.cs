using System.ComponentModel.DataAnnotations;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	public class TypeShiftBaseDto
	{
		public string Color { get; set; } = "";

		[Key]
		public int Id { get; set; }

		public bool IsVisibleInMainWindow { get; set; }

		[Required]
		public string Name { get; set; } = "";
	}
}
