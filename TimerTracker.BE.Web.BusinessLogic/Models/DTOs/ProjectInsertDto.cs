using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ProjectInsertDto : IProjectInsert
	{
		/// <inheritdoc />
		[SwaggerSchema(Description = "Popis projektu.", Nullable = true, Title = "Test")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual string? Description { get; set; }
		/// <inheritdoc />
		[SwaggerSchema(Description = "Název projektu.")]
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		[MaxLength(30, ErrorMessage = "Překročená maximální délka textu.")]
		[MinLength(5, ErrorMessage = "Nebylo dosaženo minimální déky textu.")]
		public virtual string Name { get; set; } = string.Empty;
	}
}
