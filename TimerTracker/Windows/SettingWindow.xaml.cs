using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TimerTracker.BE.DB.Providers;
using TimerTracker.Helpers;
using TimerTracker.Models;
using TimerTracker.Stories;

namespace TimerTracker.Windows
{
    [ObservableObject]
    public partial class SettingWindow : Window
    {
        private readonly MainStory _mainStory;
        private readonly ProjectProvider _projectProvider;

        private ProjectListBox _selectProjectListBox;

        private List<string> PositionList = new();

        public SettingWindow(MainStory mainStory)
        {
            _mainStory = mainStory;
            _projectProvider = _mainStory.ContainerStore.GetProjectProvider();

            InitializeComponent();

            ProjectListBox = new ObservableCollection<ProjectListBox>();
            SubModuleListBox = new ObservableCollection<SubModuleListBox>();

            loadProjects();
            setProjectItemsView();
            setSubModulesItemsView();

            CmdProjectSave = new RelayCommand(onProjectSave_Click);
            CmdSubModuleSave = new RelayCommand(onSubModuleSave_Click);

            DataContext = this;

            PositionList.Add("H:Left-V:Top");
            PositionList.Add("H:Left-V:Center");
            PositionList.Add("H:Left-V:Bottom");

            PositionList.Add("H:Center-V:Top");
            PositionList.Add("H:Center-V:Center");
            PositionList.Add("H:Center-V:Bottom");

            PositionList.Add("H:Rght-V:Top");
            PositionList.Add("H:Right-V:Center");
            PositionList.Add("H:Right-V:Bottom");

            cmbPositionList.ItemsSource = PositionList;
        }

        public ICollection<SubModuleListBox> SubModuleListBox { get; set; }

        private void listProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ProjectListBox)ProjectItemsView.CurrentItem;

            if (selected != null)
            {
                var list = _projectProvider.GetSubModules(selected.Id);

                SubModuleListBox.Clear();
                foreach (var item in list.Select(x => new SubModuleListBox(x)).ToList())
                {
                    SubModuleListBox.Add(item);
                }
            }
        }

        private void loadProjects()
        {
            var list = _projectProvider.GetProjects();
            ProjectListBox = new ObservableCollection<ProjectListBox>(list.Select(x => new ProjectListBox(x)).ToList());

            //ProjectListBox = ProjectListBox.OrderBy(item => string.IsNullOrEmpty(item.Name)) // Prázdné položky na konec
            //			.ThenBy(item => item.Name) // Abecední řazení
            //			.ToList();
        }

        private void onProjectAdd_Click(object sender, RoutedEventArgs e)
        {
            var name = $"New Project({ProjectListBox.Count + 1})";
            var item = new ProjectListBox(name, "");
            item.IsEditable = true;

            ProjectListBox.Add(item);
            ProjectItemsView.Refresh();
        }

        private void onProjectDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = (ProjectListBox)ProjectItemsView.CurrentItem;
            if (selected != null)
            {
                var result = _projectProvider.DeleteProject(selected);

                if (result != null)
                {
                    var item = ProjectListBox.First(x => x.GuidId == selected.GuidId);
                    ProjectListBox.Remove(selected);
                    ProjectItemsView.Refresh();
                }
            }
        }

        private void onProjectEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = (ProjectListBox)ProjectItemsView.CurrentItem;

            if (selected != null)
            {
                selected.IsEditable = true;
                ProjectItemsView.Refresh();
            }
        }

        private void onProjectItemsViewChangeHandler(object sender, EventArgs args)
        {
            setLblProjectInfo();
        }

        private void onProjectSave_Click(object parameter)
        {
            var item = (ProjectListBox)parameter;
            item.IsEditable = false;

            var result = _projectProvider.SaveProject(item);
            if (result != null)
            {
                var updateItem = ProjectListBox.FirstOrDefault(x => x.GuidId == item.GuidId);
                if (updateItem != null)
                {
                    updateItem.Id = result.Id;
                    ProjectItemsView.Refresh();
                }
            }
        }

        private void onSubModuleAdd_Click(object sender, RoutedEventArgs e)
        {
            var name = $"New SubModule{(SubModuleListBox.Count + 1)}";
            var selectProjectId = ((ProjectListBox)ProjectItemsView.CurrentItem).Id;
            var item = new SubModuleListBox(0, name, "", selectProjectId);
            item.IsEditable = true;

            SubModuleListBox.Add(item);
            SubModuleItemsView.Refresh();
        }

        private void onSubModuleDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem;
            if (selected != null)
            {
                var result = _projectProvider.DeleteSubModule(selected);
                if (result != null)
                {
                    var item = ProjectListBox.FirstOrDefault(x => x.GuidId == selected.GuidId);
                    SubModuleListBox.Remove(selected);
                    SubModuleItemsView.Refresh();
                }
            }
        }

        private void onSubModuleEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem;
            if (selected != null)
            {
                selected.IsEditable = true;
                SubModuleItemsView.Refresh();
            }
        }

        private void onSubModuleItemsViewChangeHandler(object sender, EventArgs args)
        {
            setLblSubModuleInfo();
        }

        private void onSubModuleSave_Click(object parameter)
        {
            var item = (SubModuleListBox)parameter;
            item.IsEditable = false;

            var result = _projectProvider.SaveSubModule(item);
            if (result != null)
            {
                var updateItem = SubModuleListBox.FirstOrDefault(x => x.GuidId == item.GuidId);
                if (updateItem != null)
                {
                    updateItem.Id = result.Id;
                    ProjectItemsView.Refresh();
                }
            }

            SubModuleItemsView.Refresh();
        }

        private void setLblProjectInfo()
        {
            lblProjectInfo.Content = ProjectListBox.Count();
        }

        private void setLblSubModuleInfo()
        {
            lblSubModuleInfo.Content = SubModuleListBox.Count();
        }

        private void setProjectItemsView()
        {
            ProjectItemsView = CollectionViewSource.GetDefaultView(ProjectListBox);
            ProjectItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            ProjectItemsView.Refresh();

            ProjectItemsView.CollectionChanged += onProjectItemsViewChangeHandler;

            setLblProjectInfo();
        }

        private void setSubModulesItemsView()
        {
            SubModuleItemsView = CollectionViewSource.GetDefaultView(SubModuleListBox);
            SubModuleItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            SubModuleItemsView.Refresh();

            SubModuleItemsView.CollectionChanged += onSubModuleItemsViewChangeHandler;

            setLblSubModuleInfo();
        }

        #region MVVM

        public SubModuleListBox _selectSubModuleListBox;
        public ICommand CmdProjectSave { get; }
        public ICommand CmdSubModuleSave { get; }
        public ICollectionView ProjectItemsView { get; set; }

        public ICollection<ProjectListBox> ProjectListBox { get; set; }

        public ProjectListBox SelectProjectListBox
        {
            get => _selectProjectListBox;
            set
            {
                if (_selectProjectListBox != value)
                {
                    _selectProjectListBox = value;
                    OnPropertyChanged();

                    var isSelected = (value != null);
                    btnProjectEdit.IsEnabled = isSelected;
                    btnProjectDelete.IsEnabled = isSelected;
                    btnSubModuleAdd.IsEnabled = isSelected && !SelectProjectListBox.IsEditable;
                }
            }
        }

        public SubModuleListBox SelectSubModuleListBox
        {
            get => _selectSubModuleListBox;
            set
            {
                if (_selectSubModuleListBox != value)
                {
                    _selectSubModuleListBox = value;
                    OnPropertyChanged();

                    var isSelected = (value != null);
                    btnSubModuleDelete.IsEnabled = isSelected;
                    btnSubModuleEdit.IsEnabled = isSelected;
                }
            }
        }

        public ICollectionView SubModuleItemsView { get; set; }

        #endregion MVVM
    }
}