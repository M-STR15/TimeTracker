using TimeTracker.Basic.Enums;

namespace TimeTracker.Web.Blazor.Server.Models
{
	public class RecordListViewModel
	{
		public string? Description { get; set; }
		public Guid? ShiftGuidId { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime? EndDateTime { get; set; }
		public Guid GuidId { get; set; }
		public string ShiftDateStr { get; set; } = string.Empty;

		public int ActivityId { get; set; }

		public string ActivityName { get; set; } = string.Empty;

		public string ProjectName { get; set; } = string.Empty;

		public string SubModuleName { get; set; } = string.Empty;

		public string TypeShiftName { get; set; } = string.Empty;

		public string Time
		{
			get
			{
				TimeSpan? time = null;
				if (ActivityId != (int)eActivity.Stop)
				{
					time = (DateTime.Now - StartDateTime);
				}
				return time?.ToString(@"hh\:mm\:ss") ?? "00:00:00";
			}
		}

	}
}
