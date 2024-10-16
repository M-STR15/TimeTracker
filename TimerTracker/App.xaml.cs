using Ninject;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Windows;
using TimerTracker.DataAccess;

namespace TimerTracker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IKernel _container;


		public App()
		{ }

		[STAThread]
		protected override void OnStartup(StartupEventArgs e)
		{
			configureContainer();

			var aaa= _container.Get<MainDatacontext>(); 
			Current.MainWindow = _container.Get<MainWindow>();
			Current.MainWindow.Show();
		}

		private void configureContainer()
		{
			_container = new StandardKernel();

			_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();
			_container.Bind<MainWindow>().To<MainWindow>().InSingletonScope();
		}
	}

}
