using System.ComponentModel.DataAnnotations;
using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(SubModule))]
	public class SubModuleBaseDto : SubModuleInsertDto, ISubModuleBase
	{
		/// <inheritdoc />
		public int Id { get; set; }
	}
}
