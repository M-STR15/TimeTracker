using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimerTracker.BE.DB.Models;
using TimerTracker.BE.DB.Models.Enums;
using TimerTracker.BE.DB.Providers;
using TimerTracker.Models;
using TimerTracker.Stories;
using TimerTracker.ViewModels;

namespace TimerTracker.Windows
{
    public partial class MainWindow : Window
    {
        private readonly MainStory _mainStory;
        private DispatcherTimer _dispatcherTimer;
        private RecordActivity _lastRecordActivity;
        private ProjectProvider _projectProvider;
        private RecordProvider _recordProvider;
        private List<ShiftCmb> _shiftCmbs;

        private ShiftProvider _shiftProvider;
        public MainWindow(MainStory mainStory)
        {
            this.DataContext = new BaseViewModel("Timer tracker");
            InitializeComponent();
            _mainStory = mainStory;

            _shiftProvider = _mainStory.ContainerStore.GetShiftProvider();
            _projectProvider = _mainStory.ContainerStore.GetProjectProvider();
            _recordProvider = _mainStory.ContainerStore.GetRecordProvider();

            loadProjects();
            loadShifts();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;

            cmbProjects.DisplayMemberPath = "Name";
            cmbShift.DisplayMemberPath = "StartDateStr";
            cmbSubModule.DisplayMemberPath = "Name";
        }

        private void _dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            setlblTime();
        }

        private bool addActivite(RecordActivity recordActivity)
        {
            return addActivite(recordActivity.Activity, recordActivity.Project, recordActivity.SubModule, recordActivity.Shift, recordActivity?.Description ?? "");
        }

        private bool addActivite(Activity activity, Project? project = null, SubModule? subModule = null, Shift? shift = null, string description = "")
        {
            try
            {
                var startTimeActivity = DateTime.Now;

                var record = new RecordActivity();
                if (shift != null && shift.GuidId != Guid.Empty)
                    record = new RecordActivity(startTimeActivity, activity.Id, shift.GuidId, project?.Id ?? null, subModule?.Id ?? null, description);
                else
                    record = new RecordActivity(startTimeActivity, activity.Id, project?.Id ?? null, subModule?.Id ?? null, description);

                var result = _recordProvider.SaveRecord(record);
                if (result != null)
                {
                    _lastRecordActivity = new RecordActivity(result.GuidId, startTimeActivity, activity, project, subModule, shift, description);
                }

                return result != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            var startTimeActivity = DateTime.Now;
            var activity = new Activity()
            {
                Id = (int)eActivity.Start,
                Name = eActivity.Start.ToString()
            };

            var selProject = (Project)cmbProjects.SelectedItem;
            var selShift = (ShiftCmb)cmbShift.SelectedItem;
            var selSubmodule = (SubModule)cmbSubModule.SelectedItem;

            var description = getTextFromRichTextBox(rtbDescription);
            var selRecordActivity = new RecordActivity(startTimeActivity, activity, selProject, selSubmodule, selShift, description);

            var result = addActivite(selRecordActivity);
            if (result)
            {
                rtbDescription.Document.Blocks.Clear();
                changeLabels();
                startTimer();
            }
        }

        private void btnEndShift_Click(object sender, RoutedEventArgs e)
        {
            var activity = new Activity()
            {
                Id = (int)eActivity.Stop,
                Name = eActivity.Stop.ToString()
            };

            var result = addActivite(activity);
            if (result)
            {
                changeLabels();
                _dispatcherTimer.Stop();
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            var activity = new Activity()
            {
                Id = (int)eActivity.Pause,
                Name = eActivity.Pause.ToString()
            };

            var result = addActivite(activity);
            if (result)
            {
                changeLabels();
                startTimer();
            }
        }

        private void cmbProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var projectId = ((Project)cmbProjects.SelectedItem).Id;
            var subModules = _projectProvider.GetSubModules(projectId);
            cmbSubModule.ItemsSource = subModules;
            if (subModules.Count > 0)
                cmbSubModule.SelectedIndex = 0;
        }

        private string getTextFromRichTextBox(RichTextBox richTextBox)
        {
            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            return textRange.Text;
        }

        private void changeLabels()
        {
            setlblTime();
            lblActivity.Text = _lastRecordActivity.Activity?.Name ?? "";
            lblProject.Text = _lastRecordActivity.Project?.Name ?? "";
            lblSubModule.Text = _lastRecordActivity.SubModule?.Name ?? "";
            lblStartTime_time.Text = _lastRecordActivity?.StartTime.ToString("HH:mm:ss");
            lblStartTime_date.Text = _lastRecordActivity?.StartTime.ToString("dd.MM.yy");

            var shift = _lastRecordActivity?.Shift;

            if (shift != null)
            {
                var cmdShift = new ShiftCmb(shift);
                lblShift_date.Text = cmdShift?.StartDateStr ?? "";
            }
            else
            {
                lblShift_date.Text = "";
            }
        }

        private void loadProjects()
        {
            cmbProjects.ItemsSource = null;
            cmbProjects.ItemsSource = _projectProvider.GetProjects();
            cmbProjects.SelectedIndex = 0;
        }

        private void loadShifts()
        {
            var currentDate = DateTime.Now;
            var getList = _shiftProvider.GetShifts(currentDate.AddDays(-7), currentDate.AddDays(3));
            _shiftCmbs = getList.Select(x => new ShiftCmb(x)).OrderByDescending(x => x.StartDate).ToList();
            _shiftCmbs.Add(new ShiftCmb());
            cmbShift.ItemsSource = _shiftCmbs.OrderByDescending(x => x.StartDate);
            cmbShift.SelectedIndex = 0;
        }
        private void mbtnRecordList_Click(object sender, RoutedEventArgs e)
        {
            var report = new RecordListWindow(_mainStory);
            report.Show();
        }

        private void mbtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new SettingWindow(_mainStory);
            window.ShowDialog();
        }

        private void mbtnShifts_Click(object sender, RoutedEventArgs e)
        {
            var window = new ShiftsPlanWindow(_mainStory);
            var result = window.ShowDialog();

            loadShifts();

            if (_lastRecordActivity.Shift != null)
            {
                var curretnSeletDate = _lastRecordActivity.Shift.StartDate;
                cmbShift.SelectedItem = _shiftCmbs.FirstOrDefault(x => x.StartDate == curretnSeletDate);
            }
        }

        private void setlblTime()
        {
            var time = (DateTime.Now - _lastRecordActivity.StartTime);
            lblTime_time.Text = time.ToString(@"hh\:mm\:ss");
        }
        private void startTimer()
        {
            if (!_dispatcherTimer.IsEnabled)
                _dispatcherTimer.Start();
        }
    }
}