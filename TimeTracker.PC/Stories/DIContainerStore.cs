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
	public class DIContainerStore
	{
		private IKernel _container = new StandardKernel();

		public DIContainerStore()
		{
			configureContainer();
		}

		private void configureContainer()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			var DbPath = Path.Join(path, "TimeTracker.db");

			//_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();
			// Načtení konfigurace
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			//var DbPath = configuration.GetSection("ConnectionString")["Database"];
			// Vytvoření DbContextOptions
			var options = new DbContextOptionsBuilder<MainDatacontext>()
				.UseSqlite($"Data Source={DbPath}")
				.Options;


			_container.Bind<IConfiguration>().ToConstant(configuration);
			_container.Bind<DbContextOptions<MainDatacontext>>().ToConstant(options);

			var services = new ServiceCollection();
			services.AddTimeTrackerBeDdService();
			services.AddToNinject(_container);

			_container.Bind<Func<MainDatacontext>>().ToMethod(ctx => new Func<MainDatacontext>(() => ctx.Kernel.Get<MainDatacontext>()));
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