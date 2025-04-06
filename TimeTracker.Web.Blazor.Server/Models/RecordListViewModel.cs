namespace TimeTracker.Web.Blazor.Server.Models
{
	public class RecordListViewModel
	{
		//public int ActivityId { get; set; }
		public string? Description { get; set; }
		//public int? ProjectId { get; set; }
		public Guid? ShiftGuidId { get; set; }
		public DateTime StartDateTime { get; set; }
		//public int? SubModuleId { get; set; }
		//public int? TypeShiftId { get; set; }
		public DateTime? EndDateTime { get; set; }
		public Guid GuidId { get; set; }

		public string ActivityName { get; set; }

		public string ProjectName { get; set; }

		public string SubModuleName { get; set; }

		public string TypeShiftName { get; set; }

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
