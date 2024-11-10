using System.Windows;
using TimeTracker.Stories;
using TimeTracker.Windows.Reports.Services;

namespace TimeTracker.Windows.Reports
{
    internal struct reportObj()
    {
        public string Activity { get; set; }
        public string Description { get; set; }
        public string EndTime { get => EndTimeDt.ToString("HH:mm:ss dd.MM.yyyy"); }
        public string Project { get; set; }
        public string StartTime { get => StartTimeDt.ToString("HH:mm:ss dd.MM.yyyy"); }
        public string TotalTime { get => TotalTimeTs.ToString(@"hh\:mm\:ss"); }
        internal DateTime EndTimeDt { get; set; }
        internal DateTime StartTimeDt { get; set; }
        internal TimeSpan TotalTimeTs { get => EndTimeDt - StartTimeDt; }
    }

    public partial class RecordListWindow : Window
    {
        private MainStory _mainStoru;
        public RecordListWindow(MainStory mainStore)
        {
            InitializeComponent();
            _mainStoru = mainStore;

            var reportParametersService = new ReportParameterService();
            cmbMonth.ItemsSource = reportParametersService.Monts;
            cmbMonth.SelectedIndex = 0;
        }
        private void onCmbMonth_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            createChart();
        }

        private void createChart()
        {
            if (cmbMonth.Text != "")
            {
                var startTime = Convert.ToDateTime("1." + cmbMonth.SelectedItem);
                var endTime = startTime.AddMonths(1);
                var origList = _mainStoru.ContainerStore.GetRecordProvider().GetRecords(startTime, endTime);
                if (origList != null)
                {
                    var list = origList.Select((record, index) => new reportObj()
                    {
                        StartTimeDt = record.StartTime,
                        EndTimeDt = (origList.Count != (index + 1) ? origList[index + 1].StartTime : DateTime.Now),
                        Activity = record.Activity.Name,
                        Project = record?.Project?.Name ?? "",
                        Description = record?.Description ?? "",
                    });
                    dtgRecordActivities.ItemsSource = list;
                }

                lblCount.Content = (origList?.Count ?? 0).ToString();
            }
        }
    }
}