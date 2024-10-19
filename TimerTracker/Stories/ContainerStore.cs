using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerTracker.DataAccess;
using TimerTracker.Providers;
using TimerTracker.Windows;

namespace TimerTracker.Stories
{
	public class ContainerStore
	{
		private IKernel _container;

		public ContainerStore()
		{
			configureContainer();
		}

		private void configureContainer()
		{
			_container = new StandardKernel();

			_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();

			_container.Bind<MainWindow>().To<MainWindow>().InSingletonScope();
			_container.Bind<RecordListWindow>().To<RecordListWindow>().InSingletonScope();

			_container.Bind<DatabaseProvider>().To<DatabaseProvider>().InSingletonScope();
			_container.Bind<ShiftProviders>().To<ShiftProviders>().InSingletonScope();
		}

		public MainWindow GetMainWindow()
		{
			return _container.Get<MainWindow>();
		}

		public DatabaseProvider GetDatabaseProvider()
		{
			return _container.Get<DatabaseProvider>();
		}

		public ShiftProviders GetShiftProviders()
		{
			return _container.Get<ShiftProviders>();
		}
	}
}
