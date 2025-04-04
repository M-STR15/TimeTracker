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
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		public int Id { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		public bool IsVisibleInMainWindow { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		[MinLength(5, ErrorMessage = "Nebylo dosaženo minimální déky textu.")]
		[MaxLength(30, ErrorMessage = "Překročená maximální délka textu.")]
		public string Name { get; set; } = string.Empty;
	}
}
