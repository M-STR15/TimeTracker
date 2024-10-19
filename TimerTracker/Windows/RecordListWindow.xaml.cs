using System.Windows;
using TimerTracker.Providers;
using TimerTracker.Stories;

namespace TimerTracker.Windows
{
	public partial class RecordListWindow : Window
	{
		public RecordListWindow(MainStory mainStore)
		{
			InitializeComponent();
			var origList = mainStore.ContainerStore.GetRecordProvider().GetRecords();
			var list = origList.Select((record, index) => new reportObj()
			{
				StartTimeDt = record.StartTime,
				EndTimeDt = (origList.Count != (index + 1) ? origList[index + 1].StartTime : DateTime.Now),
				Activity = record.Activity.Name,
				Project = record.Project.Name,
				Description = record.Description,
			});

			dtgRecordActivities.ItemsSource = list;
			lblCount.Content = origList.Count.ToString();
		}
	}

	internal struct reportObj()
	{
		public string StartTime { get=> StartTimeDt.ToString("HH:mm:ss dd.MM.yyyy");}
		internal DateTime StartTimeDt { get; set; }
		public string EndTime { get => EndTimeDt.ToString("HH:mm:ss dd.MM.yyyy"); }
		internal DateTime EndTimeDt { get; set; }
		public string TotalTime { get => TotalTimeTs.ToString(@"hh\:mm\:ss"); }
		internal TimeSpan TotalTimeTs { get => EndTimeDt - StartTimeDt; }
		public string Activity { get; set; }
		public string Project { get; set; }
		public string Description { get; set; }
	}
}
