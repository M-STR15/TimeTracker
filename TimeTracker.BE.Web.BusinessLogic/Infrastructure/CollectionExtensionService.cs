using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Infrastructure;
using TimeTracker.BE.Web.BusinessLogic.Controllers;
using TimeTracker.BE.Web.BusinessLogic.MappingProfiles;

namespace TimeTracker.BE.Web.BusinessLogic.Infrastructure
{
	public static class CollectionExtensionService
	{
		const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";
		public static IServiceCollection AddTimeTrackerBeWebSharedBusinessLogic<T>(
		this IServiceCollection services,
		bool useInMemoryDatabase = false) // Parametr pro volbu mezi InMemory a SQL Server
			where T : MainDatacontext
		{
			if (useInMemoryDatabase)
			{
				services.AddDbContext<InMemoryDbContext>(options =>
				{
					options.UseInMemoryDatabase("TestDb");
				}, ServiceLifetime.Scoped);
			}
			else
			{
				services.AddDbContext<MsSqlDbContext>(options =>
				{
					options.UseSqlServer(connectionString)
					   .EnableSensitiveDataLogging()
					   .LogTo(Console.WriteLine);

				}, ServiceLifetime.Scoped);

			}

			services.AddScoped<Func<T>>(provider => () => provider.GetRequiredService<T>());

			services.AddTimeTrackerBeDd<T>();

			services.AddScoped<ProjectController<T>>();
			services.AddScoped<ShiftController<T>>();

			services.AddAutoMapper(typeof(MappingProfile));

			// Automatické vytvoření databáze
			using (var serviceProvider = services.BuildServiceProvider())
			using (var scope = serviceProvider.CreateScope())
			{
				try
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<T>();
					if (useInMemoryDatabase)
						dbContext.Database.EnsureCreated();
					else
						dbContext.Database.Migrate();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Chyba při migraci databáze: {ex.Message}");
				}
			}

			return services;
		}
	}
}