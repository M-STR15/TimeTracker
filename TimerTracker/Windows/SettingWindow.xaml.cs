using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TimerTracker.BE.DB.Providers;
using TimerTracker.Models;
using TimerTracker.Stories;

namespace TimerTracker.Windows
{
	public partial class SettingWindow : Window, INotifyPropertyChanged
	{
		private readonly MainStory _mainStory;
		private readonly ProjectProvider _projectProvider;

		private ProjectListBox _selectProjectListBox;
		public ProjectListBox SelectProjectListBox
		{
			get => _selectProjectListBox;
			set
			{
				OnPropertyChanged(nameof(SelectProjectListBox));
			}
		}
		public SettingWindow(MainStory mainStory)
		{
			_mainStory = mainStory;
			_projectProvider = _mainStory.ContainerStore.GetProjectProvider();

			InitializeComponent();

			ProjectListBox = new ObservableCollection<ProjectListBox>();
			SubModuleListBox = new ObservableCollection<SubModuleListBox>();
			DataContext = this;

			loadProjects();
			setProjectItemsView();
			setSubModulesItemsView();
		}

		private void setProjectItemsView()
		{
			ProjectItemsView = CollectionViewSource.GetDefaultView(ProjectListBox);
			ProjectItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
			ProjectItemsView.Refresh();
		}

		private void setSubModulesItemsView()
		{
			SubModuleItemsView = CollectionViewSource.GetDefaultView(SubModuleListBox);
			SubModuleItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
			SubModuleItemsView.Refresh();
		}

		public ICollectionView ProjectItemsView { get; set; }
		public ICollection<ProjectListBox> ProjectListBox { get; set; }
		public ICollectionView SubModuleItemsView { get; set; }
		public ICollection<SubModuleListBox> SubModuleListBox { get; set; }
		private void btnProjectAdd_Click(object sender, RoutedEventArgs e)
		{
			var item = new ProjectListBox("", "");

			ProjectListBox.Add(item);

			ProjectItemsView.Refresh();
		}

		private void btnProjectEdit_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var item = (ProjectListBox)btn.Tag;
			item.IsEditable = true;

			ProjectItemsView.Refresh();
		}

		private void btnProjectSave_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var item = (ProjectListBox)btn.Tag;
			item.IsEditable = false;

			_projectProvider.SaveProject(item);
			ProjectItemsView.Refresh();
		}

		private void btnSubModuleAdd_Click(object sender, RoutedEventArgs e)
		{
			//var selectionProject = SelectProjectListBox;
			//if (selectionProject != null)
			//{
				var item = new SubModuleListBox(0, "", "",1);

				SubModuleListBox.Add(item);
				_projectProvider.SaveSubModule(item);
				SubModuleItemsView.Refresh();
			//}
		}

		private void btnSubModuleSave_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var item = (SubModuleListBox)btn.Tag;
			item.IsEditable = false;

			_projectProvider.SaveSubModule(item);
			SubModuleItemsView.Refresh();
		}

		private void btnubModuleEdit_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var item = (SubModuleListBox)btn.Tag;
			item.IsEditable = true;

			SubModuleItemsView.Refresh();
		}


































































































































































































































































































































































































































































































		 
		private void listProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selected = listProject.SelectedItem as ProjectListBox;
			var list = _projectProvider.GetSubModules(selected?.Id ?? 0);

			SubModuleListBox.Clear();
			foreach (var item in list.Select(x => new SubModuleListBox(x)).ToList())
			{
				SubModuleListBox.Add(item);

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
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
