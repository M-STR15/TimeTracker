using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ninject;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.DB.Services;
using TimeTracker.PC.Windows;
using TimeTracker.PC.Windows.Reports;

namespace TimeTracker.PC.Stories
{
	public class ContainerStore
	{
		private IKernel _container = new StandardKernel();

		public ContainerStore()
		{
			configureContainer();
		}

		private void configureContainer()
		{
			//_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();

			_container.Bind<MainWindow>().To<MainWindow>().InSingletonScope();
			_container.Bind<RecordListWindow>().To<RecordListWindow>().InSingletonScope();

			_container.Bind<RecordRepository>().To<RecordRepository>().InSingletonScope();
			_container.Bind<ShiftRepository>().To<ShiftRepository>().InSingletonScope();


			IServiceCollection services = new ServiceCollection();
			services.AddTimeTrackerBeDdService();
			services.AddToNinject(_container);
		}

		public MainWindow GetMainWindow() => _container.Get<MainWindow>();

		public RecordRepository GetDatabaseProvider() => _container.Get<RecordRepository>();

		public ShiftRepository GetShiftProvider() => _container.Get<ShiftRepository>();

		public ActivityRepository GetActivityProvider() => _container.Get<ActivityRepository>();

		public ProjectRepository GetProjectProvider() => _container.Get<ProjectRepository>();

		public RecordRepository GetRecordProvider() => _container.Get<RecordRepository>();

		public ReportRepository GetReportProvider() => _container.Get<ReportRepository>();
	}
}