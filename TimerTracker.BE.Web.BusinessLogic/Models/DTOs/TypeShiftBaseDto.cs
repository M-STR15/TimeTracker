using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class TypeShiftBaseDto : ITypeShiftBase
	{
		/// <inheritdoc />
		public string Color { get; set; } = string.Empty;
		/// <inheritdoc />
		[Key]
		public int Id { get; set; }
		/// <inheritdoc />
		public bool IsVisibleInMainWindow { get; set; }
		/// <inheritdoc />
		[Required]
		[StringLength(30, ErrorMessage = "Byla překročena dovolená délka textu.")]
		public string Name { get; set; } = string.Empty;
	}
}
