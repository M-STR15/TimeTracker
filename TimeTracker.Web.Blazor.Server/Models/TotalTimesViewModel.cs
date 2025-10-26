using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Basic.Enums;

namespace TimeTracker.Web.Blazor.Server.Models
{
	public class TotalTimesViewModel : TotalTimesDto
	{
		private const string _formTime = @"hh\:mm\:ss";

		public string ActualTimeStr { get => this?.ActualTime.ToString(_formTime) ?? string.Empty; }

		public string WorkTimeStr { get => WorkTime.ToString(_formTime); }
		public string PauseTimeStr { get => PauseTime.ToString(_formTime); }
		public string TotalTimeStr { get => TotalTime.ToString(_formTime); }

		public string WorkShiftTimeStr { get => WorkShiftTime.ToString(_formTime); }
		public string PauseShiftTimeStr { get => PauseShiftTime.ToString(_formTime); }
		public string TotalShiftTimeStr { get => TotalShiftTime.ToString(_formTime); }

		public void Update()
		{
			var addTime = new TimeSpan(0, 0, 0, 1);
			if (ActivityId != (int)eActivity.Stop)
			{
				ActualTime = ActualTime.Add(addTime);


				if (ActivityId == (int)eActivity.Start)
					WorkTime = WorkTime.Add(addTime);
				else if (ActivityId == (int)eActivity.Pause)
					PauseTime = PauseTime.Add(addTime);

				TotalTime = TotalTime.Add(addTime);


				if (ActivityId == (int)eActivity.Start)
					WorkShiftTime = WorkShiftTime.Add(addTime);
				else if (ActivityId == (int)eActivity.Pause)
					PauseShiftTime = PauseShiftTime.Add(addTime);

				TotalShiftTime = TotalShiftTime.Add(addTime);
			}
		}

		public void UpdateActivityId(eActivity eActivity)
		{
			ActivityId = (int)eActivity;
		}
	}
}
