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
	public partial class SettingWindow : Window
	{
		private readonly MainStory _mainStory;
		private readonly ProjectProvider _projectProvider;
		public ICollection<ProjectListBox> ProjectListBox { get; set; }

		public ICollectionView ItemsView { get; set; }

		public SettingWindow(MainStory mainStory)
		{
			_mainStory = mainStory;
			_projectProvider = _mainStory.ContainerStore.GetProjectProvider();

			InitializeComponent();

			ProjectListBox = new ObservableCollection<ProjectListBox>();

			loadProjects();
			DataContext = this;

			ItemsView = CollectionViewSource.GetDefaultView(ProjectListBox);
			ItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
		}

		private void loadProjects()
		{
			var list = _projectProvider.GetProjects();
			ProjectListBox = new ObservableCollection<ProjectListBox>(list.Select(x => new ProjectListBox(x)).ToList());
		}

		private void listProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = listProject.SelectedItem as ProjectListBox;
			listSubModule.ItemsSource = _projectProvider.GetSubModules(item.Id);
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var item = (ProjectListBox)btn.Tag;
			item.IsEditable = false;

			var aa = ProjectListBox.Remove(item);
			ProjectListBox.Add(item);
		}
	}
}
