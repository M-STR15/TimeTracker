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

		public string ActivityName { get; set; } = string.Empty;

		public string ProjectName { get; set; } = string.Empty;

		public string SubModuleName { get; set; } = string.Empty;

		public string TypeShiftName { get; set; } = string.Empty;

		public string Time
		{
			get
			{
				var time = (DateTime.Now - StartDateTime);
				var actualActivityInSeconds = time.TotalSeconds;
				return time.ToString(@"hh\:mm\:ss");
			}
		}

	}
}
