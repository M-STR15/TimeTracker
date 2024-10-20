using System.Collections.ObjectModel;
using System.Windows;
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
		public SettingWindow(MainStory mainStory)
		{
			_mainStory = mainStory;
			_projectProvider = _mainStory.ContainerStore.GetProjectProvider();

			InitializeComponent();

			ProjectListBox = new ObservableCollection<ProjectListBox>();

			loadProjects();
			DataContext = this;
		}

		private void loadProjects()
		{
			var list = _projectProvider.GetProjects();
			ProjectListBox = new ObservableCollection<ProjectListBox>(list.Select(x => new ProjectListBox(x)).ToList());
		}

		private void listProject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var item = listProject.SelectedItem as ProjectListBox;
			listSubModule.ItemsSource = _projectProvider.GetSubModules(item.Id);
		}

	}
}
