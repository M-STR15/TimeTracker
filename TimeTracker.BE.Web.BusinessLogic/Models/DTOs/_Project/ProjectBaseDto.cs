using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ProjectBaseDto : ProjectInsertDto, IProjectBase
	{
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		[SwaggerSchema(Description = "Identifikátor projektu.")]
		public virtual int Id { get; set; }
	}
}