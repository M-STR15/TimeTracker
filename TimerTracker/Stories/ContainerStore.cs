﻿using Ninject;
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

			_container.Bind<RecordProvider>().To<RecordProvider>().InSingletonScope();
			_container.Bind<ShiftProvider>().To<ShiftProvider>().InSingletonScope();
		}

		public MainWindow GetMainWindow()
		{
			return _container.Get<MainWindow>();
		}

		public RecordProvider GetDatabaseProvider()
		{
			return _container.Get<RecordProvider>();
		}

		public ShiftProvider GetShiftProvider()
		{
			return _container.Get<ShiftProvider>();
		}

		public ActivityProvider GetActivityProvider()
		{
			return _container.Get<ActivityProvider>();
		}

		public ProjectProvider GetProjectProvider()
		{
			return _container.Get<ProjectProvider>();
		}

		public RecordProvider GetRecordProvider()
		{
			return _container.Get<RecordProvider>();
		}
	}
}
