using System.ComponentModel.DataAnnotations;
using TimerTracker.BE.Web.BusinessLogic.Helpers;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	[CopyAttributesFrom(typeof(Activity))]
	public class ActivityBaseDto : IActivityBase
	{
		/// <inheritdoc />
		[Required]
		public int Id { get; set; }
		/// <inheritdoc />
		[StringLength(30,ErrorMessage ="Byla překročena dovolená délka textu.")]
		public string Name { get; set; } = string.Empty;
	}
}
