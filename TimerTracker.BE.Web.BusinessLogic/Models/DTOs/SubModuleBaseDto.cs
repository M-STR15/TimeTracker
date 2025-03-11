using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class SubModuleBaseDto : SubModuleInsertDto, ISubModuleBase
	{
		/// <inheritdoc />
		public int Id { get; set; }
	}
}
