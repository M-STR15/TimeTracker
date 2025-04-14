using TimeTracker.BE.DB.Models.Interfaces;

namespace TimeTracker.PC.Models
{
	/// <summary>
	/// Třída TotalTimes reprezentuje model pro sledování časových údajů spojených s aktivitami a směnami.
	/// </summary>
	public class TotalTimes : ITotalTimes
	{
		/// <inheritdoc />
		public int ActivityId { get; set; }

		/// <inheritdoc />
		public Guid? ShiftGuidId { get; set; }

		/// <inheritdoc />
		public TimeSpan ActualTime { get; set; }

		/// <inheritdoc />
		public TimeSpan WorkTime { get; set; }

		/// <inheritdoc />
		public TimeSpan PauseTime { get; set; }

		/// <inheritdoc />
		public TimeSpan TotalTime { get; set; }

		/// <inheritdoc />
		public TimeSpan WorkShiftTime { get; set; }

		/// <inheritdoc />
		public TimeSpan PauseShiftTime { get; set; }

		/// <inheritdoc />
		public TimeSpan TotalShiftTime { get; set; }
	}
}
