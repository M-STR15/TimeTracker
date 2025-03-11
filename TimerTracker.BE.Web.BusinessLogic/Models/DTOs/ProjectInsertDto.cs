using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ProjectInsertDto : IProjectInsert
	{
		/// <inheritdoc />
		[SwaggerSchema(Description = "Popis projektu.", Nullable = true, Title = "Test")]
		public virtual string? Description { get; set; }
		/// <inheritdoc />
		[SwaggerSchema(Description = "Název projektu.")]
		[Required]
		public virtual string Name { get; set; } = string.Empty;
	}
}
