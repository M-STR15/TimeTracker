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
        private Project? _prj;
        private ProjectProvider _projectProvider;
        private RecordProvider _recordProvider;
        private Activity? _selectActivity;
        private ShiftCmb? _selectShift;
        private List<ShiftCmb> _shiftCmbs;
        private ShiftProvider _shiftProvider;
        private DateTime _startTimeActivity;
        private SubModule? subM;

        public MainWindow(MainStory mainStory)
        {
            this.DataContext = new BaseViewModel("Timer tracker");
            InitializeComponent();
            _mainStory = mainStory;
            _projectProvider = _mainStory.ContainerStore.GetProjectProvider();
            _shiftProvider = _mainStory.ContainerStore.GetShiftProvider();
            _recordProvider = _mainStory.ContainerStore.GetRecordProvider();
            //loadActivities();
            loadProjects();
            loadShifts();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;

            cmbProjects.DisplayMemberPath = "Name";
            cmbShift.DisplayMemberPath = "StartDateStr";
            cmbSubModule.DisplayMemberPath = "Name";
        }

        private Project? _selectProject
        {
            get => _prj;
            set
            {
                _prj = value;
                var isEditBtn = (value != null);
                btnPause.IsEnabled = isEditBtn;
                btnEndShift.IsEnabled = isEditBtn;
            }
        }

        private SubModule? _selectSubModule
        {
            get => subM;
            set
            {
                subM = value;
            }
        }

        private void _dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            var time = (DateTime.Now - _startTimeActivity);
            txbTime_time.Text = time.ToString(@"hh\:mm\:ss");
        }

        private bool addActivite(Activity activity, Project? project = null, SubModule? subModule = null, ShiftCmb? shift = null, string description = "")
        {
            var startTimeActivity = DateTime.Now;

            var record = new RecordActivity();
            if (shift != null && shift.GuidId != Guid.Empty)
                record = new RecordActivity(startTimeActivity, activity.Id, shift.GuidId, project?.Id ?? null, subModule?.Id ?? null, description);
            else
                record = new RecordActivity(startTimeActivity, activity.Id, project?.Id ?? null, subModule?.Id ?? null, description);

            var result = _recordProvider.SaveRecord(record);
            if (true)
            {
                _selectActivity = activity;
                _selectProject = project;
                _startTimeActivity = startTimeActivity;
            }

            return result;
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            var activity = new Activity()
            {
                Id = (int)eActivity.Start,
                Name = eActivity.Start.ToString()
            };

            _selectProject = (Project)cmbProjects.SelectedItem;
            _selectShift = (ShiftCmb)cmbShift.SelectedItem;
            _selectSubModule = (SubModule)cmbSubModule.SelectedItem;

            var description = getTextFromRichTextBox(rtbDescription);
            var result = addActivite(activity, _selectProject, _selectSubModule, _selectShift, description);
            if (result)
            {
                changeLabels();
                _dispatcherTimer.Start();
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
                _dispatcherTimer.Start();
            }
        }

        private void cmbProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadSubModules();
        }

        private string getTextFromRichTextBox(RichTextBox richTextBox)
        {
            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            return textRange.Text;
        }

        private void changeLabels()
        {
            txbActivity.Text = _selectActivity?.Name ?? "";
            txbProject.Text = _selectProject?.Name ?? "";
            txbSubModule.Text = _selectSubModule?.Name ?? "";
            txbStartTime_time.Text = _startTimeActivity.ToString("HH:mm:ss");
            txbStartTime_date.Text = _startTimeActivity.ToString("dd.MM.yy");
            txbShift_date.Text = _selectShift?.StartDateStr ?? "";
        }

        //private void loadActivities()
        //{
        //	cmbActivities.ItemsSource = _mainStory.ContainerStore.GetActivityProvider().GetActivities();
        //	cmbActivities.DisplayMemberPath = "Name";
        //	cmbActivities.SelectedIndex = 0;
        //}
        private void loadProjects()
        {
            cmbProjects.ItemsSource = null;
            cmbProjects.ItemsSource = _projectProvider.GetProjects();
            cmbProjects.SelectedIndex = 0;
        }
        private void loadShifts()
        {
            var currentDate = DateTime.Now;
            var getList = _shiftProvider.GetShifts(currentDate.AddDays(-7), currentDate);
            _shiftCmbs = getList.Select(x => new ShiftCmb(x)).OrderByDescending(x => x.StartDate).ToList();
            _shiftCmbs.Add(new ShiftCmb());
            cmbShift.ItemsSource = _shiftCmbs.OrderByDescending(x => x.StartDate);
            cmbShift.SelectedIndex = 0;
        }

        private void loadSubModules()
        {
            cmbSubModule.ItemsSource = _projectProvider.GetSubModules();
            cmbSubModule.SelectedIndex = 0;
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

            if (_selectShift != null)
            {
                var curretnSeletDate = _selectShift.StartDate;
                cmbShift.SelectedItem = _shiftCmbs.FirstOrDefault(x => x.StartDate == curretnSeletDate);
            }
        }
    }
}