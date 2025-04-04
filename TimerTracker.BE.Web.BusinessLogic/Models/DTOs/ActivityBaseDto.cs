using System.ComponentModel.DataAnnotations;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ActivityBaseDto : IActivityBase
	{
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		public int Id { get; set; }
		/// <inheritdoc />
		[Required(ErrorMessage = "Hodnota je vyžadována")]
		public string Name { get; set; } = string.Empty;
	}
}
