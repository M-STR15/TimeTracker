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
    public partial class SettingWindow : Window, INotifyPropertyChanged
    {
        private readonly MainStory _mainStory;
        private readonly ProjectProvider _projectProvider;

        private ProjectListBox _selectProjectListBox;
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

            CmdProjectSave = new RelayCommand(projectSave_Click);
            CmdSubModuleSave = new RelayCommand(subModuleSave_Click);

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollectionView ProjectItemsView { get; set; }

        public ICollection<ProjectListBox> ProjectListBox { get; set; }

        public ProjectListBox SelectProjectListBox
        {
            get => _selectProjectListBox;
            set
            {
                OnPropertyChanged(nameof(SelectProjectListBox));
            }
        }
        public ICollectionView SubModuleItemsView { get; set; }

        public ICollection<SubModuleListBox> SubModuleListBox { get; set; }

        public ICommand CmdProjectSave { get; }
        public ICommand CmdSubModuleSave { get; }

        private ProjectListBox? _project;
        private ProjectListBox? _selectProjectLb
        {
            get => _project;
            set
            {
                if (_project != value)
                {
                    _project = value;
                    var isSelect = (_selectProjectLb == null);
                    btnEditProject.IsEnabled = !isSelect;
                    btnProjectDelete.IsEnabled = !isSelect;
                    btnSubModuleAdd.IsEnabled = !isSelect;
                }
            }
        }

        private SubModuleListBox? _subModule;
        private SubModuleListBox? _selectSubModuleLb
        {
            get => _subModule;
            set
            {
                if (_subModule != value)
                {
                    _subModule = value;
                    var isSelect = (_selectSubModuleLb == null);
                    btnSubModuleEdit.IsEnabled = !isSelect;
                    btnSubModuleDelete.IsEnabled = !isSelect;
                }
            }
        }

        private void btnProjectAdd_Click(object sender, RoutedEventArgs e)
        {
            var nameObject = $"New Project({ProjectListBox.Count + 1})";
            var item = new ProjectListBox(nameObject);
            item.IsEditable = true;

            ProjectListBox.Add(item);
            ProjectItemsView.Refresh();
        }

        private void btnProjectDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = (ProjectListBox)ProjectItemsView.CurrentItem;
            if (selected != null)
            {
                var result = _projectProvider.DeleteProject(selected);

                if (result)
                {
                    ProjectListBox.Remove(selected);
                    ProjectItemsView.Refresh();
                }
            }
        }

        private void btnProjectEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = (ProjectListBox)ProjectItemsView.CurrentItem;

            if (item != null)
            {
                item.IsEditable = true;
                ProjectItemsView.Refresh();
            }
        }

        private void projectSave_Click(object parameter)
        {
            var item = (ProjectListBox)parameter;

            if (item != null)
            {
                var result = _projectProvider.SaveProject(item);
                if (result != null)
                {
                    var updateProject = ProjectListBox.FirstOrDefault(x => x.GuidIdForListbox == item.GuidIdForListbox);
                    if (updateProject != null)
                    {
                        updateProject.Id = result.Id;
                        updateProject.IsEditable = false;
                    }
                    ProjectItemsView.Refresh();
                }
            }
        }

        private void btnSubModuleAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = (ProjectListBox)ProjectItemsView.CurrentItem;
            if (item != null)
            {
                var itemName = $"New SubModule({SubModuleListBox.Count + 1})";
                var subModule = new SubModuleListBox(0, itemName, "", item.Id);
                subModule.IsEditable = true;

                SubModuleListBox.Add(subModule);
                SubModuleItemsView.Refresh();
            }
        }

        private void btnSubModuleDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem;
            if (selected != null)
            {
                var result = _projectProvider.DeleteSubModule(selected);
                if (result)
                {
                    SubModuleListBox.Remove(selected);
                    SubModuleItemsView.Refresh();
                }
            }
        }

        private void btnSubModuleEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem;
            if (selected != null)
            {
                selected.IsEditable = true;
                SubModuleItemsView.Refresh();
            }
        }

        private void subModuleSave_Click(object parameter)
        {
            var currentItem = (SubModuleListBox)SubModuleItemsView.CurrentItem;
            var item = (SubModuleListBox)parameter;
            item.ProjectId = _selectProjectLb?.Id ?? 0;
            item.IsEditable = false;

            var result = _projectProvider.SaveSubModule(item);
            if (result != null)
            {
                var updateSubModule = SubModuleListBox.FirstOrDefault(x => x.GuidIdForListbox == item.GuidIdForListbox);
                if (updateSubModule != null)
                {
                    updateSubModule.Id = result.Id;
                    updateSubModule.IsEditable = false;
                }
                SubModuleItemsView.Refresh();
            }
        }

        private void listProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ProjectListBox)ProjectItemsView?.CurrentItem ?? null;
            _selectProjectLb = selected;
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

        private void onProjectItemsViewChangeHandler(object sender, EventArgs args)
        {
            setLblProjectInfo();
        }

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        private void onSubModuleItemsViewChangeHandler(object sender, EventArgs args)
        {
            setLblSubModuleInfo();
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

        private void listSubModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (SubModuleListBox)SubModuleItemsView.CurrentItem ?? null;
            _selectSubModuleLb = selected;
        }
    }
}
