using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.DB.Models
{
	/// <inheritdoc />
	public class ProjectBaseDto : IProjectBase
	{
		/// <inheritdoc />
		[SwaggerSchema(Description = "Popis projektu.",Nullable =true,Title ="Test")]
		public virtual string? Description { get; set; }
		/// <inheritdoc />
		[SwaggerSchema(Description = "Identifikátor projektu.")]
		public virtual int Id { get; set; }
		/// <inheritdoc />
		[SwaggerSchema(Description = "Název projektu.")]
		[Required]
		public virtual string Name { get; set; } = string.Empty;
	}
}