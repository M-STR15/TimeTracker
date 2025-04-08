using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class SubModuleBaseDto : SubModuleInsertDto, ISubModuleBase
	{
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota 'ID' je vyžadována")]
		public int Id { get; set; }
	}
}
