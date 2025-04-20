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
		public static IServiceCollection AddTimeTrackerBeWebSharedBusinessLogic(
		this IServiceCollection services,
		string connectionString,
		bool useInMemoryDatabase = false) // Parametr pro volbu mezi InMemory a SQL Server
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


			services.AddScoped<Func<MsSqlDbContext>>(provider => () => provider.GetRequiredService<MsSqlDbContext>());

			services.AddTimeTrackerBeDd<MsSqlDbContext>();

			services.AddScoped<ProjectController>();
			services.AddScoped<ShiftController>();
			services.AddAutoMapper(typeof(MappingProfile));

			// Automatické vytvoření databáze
			using (var serviceProvider = services.BuildServiceProvider())
			using (var scope = serviceProvider.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
				try
				{
					if (useInMemoryDatabase)
					{
						dbContext.Database.EnsureCreated();
					}
					else
					{
						dbContext.Database.Migrate();
					}
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