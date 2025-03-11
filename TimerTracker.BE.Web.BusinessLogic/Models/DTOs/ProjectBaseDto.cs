using Swashbuckle.AspNetCore.Annotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ProjectBaseDto : ProjectInsertDto, IProjectBase
	{
		/// <inheritdoc />
		[SwaggerSchema(Description = "Identifikátor projektu.")]
		public virtual int Id { get; set; }
	}
}