using TimeTracker.BE.DB.Models.Interfaces;

namespace TimerTracker.BE.Web.BusinessLogic.Models.DTOs
{
	/// <inheritdoc />
	public class ActivityBaseDto : IActivityBase
	{
		/// <inheritdoc />
		public int Id { get; set; }
		/// <inheritdoc />
		public string Name { get; set; } = string.Empty;
	}
}
