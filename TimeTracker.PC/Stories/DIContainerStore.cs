using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.IO;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Infrastructure;
using TimeTracker.BE.DB.Repositories;
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
			createDb();

			_container.Bind<SqliteDbContext>().To<SqliteDbContext>().InScope(ctx => ctx.Kernel);
			_container.Bind<Func<SqliteDbContext>>().ToMethod(ctx => new Func<SqliteDbContext>(() => ctx.Kernel.Get<SqliteDbContext>()));

			//var dbContext = _container.Get<SqliteDbContext>();
			//var fceDbContext = _container.Get<Func<SqliteDbContext>>();
			var services = new ServiceCollection();
			services.AddTimeTrackerBeDd<SqliteDbContext>();
			services.AddToNinject(_container);

			_container.Bind<MainWindow>().To<MainWindow>().InSingletonScope();
			_container.Bind<RecordListWindow>().To<RecordListWindow>().InSingletonScope();
		}



		private void createDb()
		{
			var dbPath = getDatabasePath();
			var options = createDbContextOptions(dbPath);
			initializeDatabase(options);
			registerForDependencyInjection(options);
		}
		/// <summary>
		/// Vytvoření cesty k databázi
		/// </summary>
		/// <returns></returns>
		private string getDatabasePath()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			return Path.Combine(path, "TimeTracker.db");
		}
		/// <summary>
		/// Vytvoření možností pro DbContext
		/// </summary>
		/// <param name="dbPath"></param>
		/// <returns></returns>
		private DbContextOptions<SqliteDbContext> createDbContextOptions(string dbPath)
		{
			return new DbContextOptionsBuilder<SqliteDbContext>()
				.UseSqlite($"Data Source={dbPath}")
				.Options;
		}
		/// <summary>
		/// Inicializace databáze, pokud neexistuje
		/// </summary>
		/// <param name="options"></param>
		private void initializeDatabase(DbContextOptions<SqliteDbContext> options)
		{
			using (var context = new SqliteDbContext(options))
			{
				// Pokud používáte migrace, zajistíte migrace
				context.Database.Migrate();
			}
		}
		/// <summary>
		/// Registrace konfigurace a možností pro DI
		/// </summary>
		/// <param name="options"></param>
		private void registerForDependencyInjection(DbContextOptions<SqliteDbContext> options)
		{
			// Předání konfigurace a možností pro DI kontejner
			var configuration = loadConfiguration();
			_container.Bind<IConfiguration>().ToConstant(configuration);
			_container.Bind<DbContextOptions<SqliteDbContext>>().ToConstant(options);
		}
		/// <summary>
		/// Načtení konfigurace
		/// </summary>
		/// <returns></returns>
		private IConfiguration loadConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}










		public MainWindow GetMainWindow() => _container.Get<MainWindow>();

		public RecordRepository<SqliteDbContext> GetDatabaseRepository() => _container.Get<RecordRepository<SqliteDbContext>>();

		public ShiftRepository<SqliteDbContext> GetShiftRepository() => _container.Get<ShiftRepository<SqliteDbContext>>();
		public TypeShiftRepository<SqliteDbContext> GetTypeShiftRepository() => _container.Get<TypeShiftRepository<SqliteDbContext>>();

		public ActivityRepository<SqliteDbContext> GetActivityRepository() => _container.Get<ActivityRepository<SqliteDbContext>>();

		public ProjectRepository<SqliteDbContext> GetProjectRepository() => _container.Get<ProjectRepository<SqliteDbContext>>();

		public SubModuleRepository<SqliteDbContext> GetSubModuleRepository() => _container.Get<SubModuleRepository<SqliteDbContext>>();

		public RecordRepository<SqliteDbContext> GetRecordRepository() => _container.Get<RecordRepository<SqliteDbContext>>();

		public ReportRepository<SqliteDbContext> GetReportRepository() => _container.Get<ReportRepository<SqliteDbContext>>();
	}
}