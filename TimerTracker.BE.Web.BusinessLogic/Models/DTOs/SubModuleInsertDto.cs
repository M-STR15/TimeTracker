using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(SubModule))]
	public class SubModuleInsertDto : ISubModuleInsert
	{
		/// <inheritdoc />
		public string? Description { get; set; }
		/// <inheritdoc />
		public string Name { get; set; } = string.Empty;
		/// <inheritdoc />
		public int ProjectId { get; set; }
	}
}
