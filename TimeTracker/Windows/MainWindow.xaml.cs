﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Models.Enums;
using TimeTracker.BE.DB.Providers;
using TimeTracker.Models;
using TimeTracker.Services;
using TimeTracker.Stories;
using TimeTracker.ViewModels;
using TimeTracker.Windows.Reports;

namespace TimeTracker.Windows
{
    public partial class MainWindow : Window
    {
        private readonly MainStory _mainStory;
        private DispatcherTimer _dispatcherTimer;
        private RecordActivity _lastRecordActivity;
        private ProjectProvider _projectProvider;
        private RecordProvider _recordProvider;
        private List<ShiftCmb> _shiftCmbs = new();
        private List<TypeShift> _typeShifts = new();
        private EventLogService _eventLogService;
        private ShiftProvider _shiftProvider;

        public MainWindow(MainStory mainStory)
        {
            this.DataContext = new BaseViewModel("Timer tracker");
            InitializeComponent();
            _eventLogService = new EventLogService();
            _mainStory = mainStory;

            _shiftProvider = _mainStory.ContainerStore.GetShiftProvider();
            _projectProvider = _mainStory.ContainerStore.GetProjectProvider();
            _recordProvider = _mainStory.ContainerStore.GetRecordProvider();

            loadProjects();
            loadShifts();
            loadTypeShifts();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;

            cmbProjects.DisplayMemberPath = "Name";
            cmbShift.DisplayMemberPath = "StartDateStr";
            cmbSubModule.DisplayMemberPath = "Name";
            cmbTypeShift.DisplayMemberPath = "Name";
        }

        private void _dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            setlblTime();
        }

        private bool addActivite(RecordActivity recordActivity)
        {
            return addActivite(recordActivity.Activity, recordActivity.TypeShift, recordActivity.Project, recordActivity.SubModule, recordActivity.Shift, recordActivity?.Description ?? "");
        }

        private bool addActivite(Activity activity, TypeShift typeShift, Project? project = null, SubModule? subModule = null, Shift? shift = null, string description = "")
        {
            try
            {
                var startTimeActivity = DateTime.Now;

                var record = new RecordActivity();
                if (shift != null && shift.GuidId != Guid.Empty)
                    record = new RecordActivity(startTimeActivity, activity.Id, shift.GuidId, typeShift.Id, project?.Id ?? null, subModule?.Id ?? null, description);
                else
                    record = new RecordActivity(startTimeActivity, activity.Id, typeShift.Id, project?.Id ?? null, subModule?.Id ?? null, description);

                var result = _recordProvider.SaveRecord(record);
                if (result != null)
                {
                    _lastRecordActivity = new RecordActivity(result.GuidId, startTimeActivity, activity, typeShift, project, subModule, shift, description);
                }

                return result != null;
            }
            catch (Exception)
            {
                return false;
            }
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

        private void loadTypeShifts()
        {
            _typeShifts = _shiftProvider.GetTypeShiftsForMainWindow();
            cmbTypeShift.ItemsSource = _typeShifts;
            cmbTypeShift.SelectedIndex = 0;
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

        private void onActionAfterClickActivate_Click(object sender, RoutedEventArgs e)
        {
            try
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
                var selTypeShift = (TypeShift)cmbTypeShift.SelectedItem;

                var description = getTextFromRichTextBox(rtbDescription);
                var selRecordActivity = new RecordActivity(startTimeActivity, activity, selTypeShift, selProject, selSubmodule, selShift, description);

                var result = addActivite(selRecordActivity);
                if (result)
                {
                    rtbDescription.Document.Blocks.Clear();
                    changeLabels();
                    startTimer();
                }
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("51905c9e-51c5-4fa4-bac2-fe60543bc170"), ex.Message, "Problém při vkláání akivity.");
            }
        }

        private void onActionAfterClickEndShift_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var activity = new Activity()
                {
                    Id = (int)eActivity.Stop,
                    Name = eActivity.Stop.ToString()
                };

                var selTypeShift = (TypeShift)cmbTypeShift.SelectedItem;

                var result = addActivite(activity, selTypeShift);
                if (result)
                {
                    changeLabels();
                    _dispatcherTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("d769b7d8-adea-4011-babe-4415f3258467"), ex.Message, "Problém při ukládání konce směny.");
            }
        }

        private void onActionAfterClickPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var activity = new Activity()
                {
                    Id = (int)eActivity.Pause,
                    Name = eActivity.Pause.ToString()
                };

                var selTypeShift = (TypeShift)cmbTypeShift.SelectedItem;

                var result = addActivite(activity, selTypeShift);
                if (result)
                {
                    changeLabels();
                    startTimer();
                }
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("aa2467f9-bddd-4c6b-a3bd-b4554936314f"), ex.Message, "Problém při ukládání pauzy.");
            }
        }

        private void onLoadDataAfterChangeProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbProjects.SelectedItem != null)
                {
                    var projectId = ((Project)cmbProjects.SelectedItem).Id;
                    var subModules = _projectProvider.GetSubModules(projectId);
                    cmbSubModule.ItemsSource = subModules;
                    if (subModules.Count > 0)
                        cmbSubModule.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("01c4c769-2c67-48ab-9c15-156609f49e0d"), ex.Message, "Problém při přepnutí projectu.");
            }
        }

        private void onOpenWindowReportRecords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var report = new RecordListWindow(_mainStory);
                report.Show();
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("9d0de4e8-d44e-4404-bf96-ca312a7556fa"), ex.Message, "Problém při otvírání okna s reportem.");
            }
        }

        private void onOpenWindowSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new SettingWindow(_mainStory);
                window.ShowDialog();

                loadProjects();
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("d070dfde-f70c-4aa3-a486-3d97d4f1688a"), ex.Message, "Problém při otvírání okna s nastavením.");
            }
        }

        private void onOpenWindowShifts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new ShiftsPlanWindow(_mainStory);
                var result = window.ShowDialog();

                loadShifts();

                if (_lastRecordActivity != null && _lastRecordActivity.Shift != null)
                {
                    var curretnSeletDate = _lastRecordActivity.Shift.StartDate;
                    cmbShift.SelectedItem = _shiftCmbs.FirstOrDefault(x => x.StartDate == curretnSeletDate);
                }
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("1234980a-9768-40b0-bbb6-9ab6927adb1a"), ex.Message, "Problém při otvírání okna se směnami.");
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

        private void mbtnActivitiesOverDays_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new ActivitiesOverDaysWindow();
                window.Show();
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("1a0e5889-4d8f-4560-8d18-51040ff5e4a4"), ex.Message, "Problém při otvírání reportu.");
            }
        }

        private void mbtnPlanVsRealitaWorkHours_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new PlanVsRealitaWorkHoursWindow();
                window.Show();
            }
            catch (Exception ex)
            {
                _eventLogService.WriteError(new Guid("ebb2c705-f5dd-4cc9-81b6-99caf462292a"), ex.Message, "Problém při otvírání reportu.");
            }
        }
    }
}