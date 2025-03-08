using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class SubModuleBaseDto : ISubModuleBase
	{
		/// <inheritdoc />
		public string? Description { get; set; }
		/// <inheritdoc />
		public int Id { get; set; }
		/// <inheritdoc />
		[Required]
		public string Name { get; set; } = string.Empty;
		/// <inheritdoc />
		public int ProjectId { get; set; }
	}
}
