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
		public int Id { get; set; }
		/// <inheritdoc />
		public string Name { get; set; } = string.Empty;
	}
}
