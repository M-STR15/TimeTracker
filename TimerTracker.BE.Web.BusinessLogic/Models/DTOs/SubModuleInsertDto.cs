using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class SubModuleInsertDto : ISubModuleInsert
	{
		/// <inheritdoc />
		public string? Description { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		[MinLength(5, ErrorMessage = "Nebylo dosaženo minimální déky textu.")]
		[MaxLength(30, ErrorMessage = "Překročená maximální délka textu.")]
		public string Name { get; set; } = string.Empty;
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		public int ProjectId { get; set; }
	}
}
