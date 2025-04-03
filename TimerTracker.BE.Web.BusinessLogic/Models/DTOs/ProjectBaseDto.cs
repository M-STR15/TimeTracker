using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(Project))]
	public class ProjectBaseDto : ProjectInsertDto, IProjectBase
	{
		/// <inheritdoc />
		[Required]
		[SwaggerSchema(Description = "Identifikátor projektu.")]
		public virtual int Id { get; set; }
	}
}