using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(Project))]
	public class ProjectInsertDto : IProjectInsert
	{
		/// <inheritdoc />
		[SwaggerSchema(Description = "Popis projektu.", Nullable = true, Title = "Test")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual string? Description { get; set; }
		/// <inheritdoc />
		[SwaggerSchema(Description = "Název projektu.")]
		public virtual string Name { get; set; } = string.Empty;
	}
}
