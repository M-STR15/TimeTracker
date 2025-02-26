using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.IO;
using TimeTracker.BE.DB.DataAccess;
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
			// Načtení konfigurace
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			// Vytvoření DbContextOptions
			var options = new DbContextOptionsBuilder<MainDatacontext>()
				.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
				.Options;


			IServiceCollection services = new ServiceCollection();
			services.AddTimeTrackerBeDdService(configuration, options);
			services.AddToNinject(_container);

			_container.Bind<MainWindow>().To<MainWindow>().InSingletonScope();
			_container.Bind<RecordListWindow>().To<RecordListWindow>().InSingletonScope();

			_container.Bind<RecordRepository>().To<RecordRepository>().InSingletonScope();
			_container.Bind<ShiftRepository>().To<ShiftRepository>().InSingletonScope();

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