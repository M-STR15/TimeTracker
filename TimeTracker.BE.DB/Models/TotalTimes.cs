namespace TimeTracker.PC.Models
{
	public class TotalTimes
	{
		public TimeSpan ActualTime { get; set; }

		public TimeSpan WorkTime { get; set; }
		public TimeSpan PauseTime { get; set; }
		public TimeSpan TotalTime { get; set; }

		public TimeSpan WorkShiftTime { get; set; }
		public TimeSpan PauseShiftTime { get; set; }
		public TimeSpan TotalShiftTime { get; set; }
	}
}
