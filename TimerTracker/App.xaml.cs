using System.Windows;
using TimerTracker.Stories;

namespace TimerTracker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private MainStory _mainStory;
		public App()
		{ }

		[STAThread]
		protected override void OnStartup(StartupEventArgs e)
		{
			_mainStory = new MainStory();

			Current.MainWindow = _mainStory.ContainerStore.GetMainWindow();
			Current.MainWindow.Show();
		}
	}

}
