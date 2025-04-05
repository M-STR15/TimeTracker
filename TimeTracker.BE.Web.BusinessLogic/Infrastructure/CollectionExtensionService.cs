using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Infrastructure;
using TimeTracker.BE.Web.BusinessLogic.Controllers;
using TimeTracker.BE.Web.BusinessLogic.MappingProfiles;

namespace TimeTracker.BE.Web.Shared.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeWebSharedBusinessLogic(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<MsSqlDbContext>(options =>
				options.UseSqlServer(connectionString)
					.EnableSensitiveDataLogging()
					.LogTo(Console.WriteLine), ServiceLifetime.Scoped);


			services.AddScoped<Func<MsSqlDbContext>>(provider => () => provider.GetRequiredService<MsSqlDbContext>());

			services.AddTimeTrackerBeDd<MsSqlDbContext>();

			services.AddScoped<ProjectController>();
			services.AddScoped<ShiftController>();
			// AutoMapper pro mapování
			services.AddAutoMapper(typeof(MappingProfile));

			// Automatické vytvoření databáze (pokud neexistuje)
			using (var serviceProvider = services.BuildServiceProvider())
			{
				using (var scope = serviceProvider.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
					try
					{
						dbContext.Database.Migrate();  // Aplikuje všechny migrace a vytvoří databázi, pokud neexistuje
						Console.WriteLine("Databáze byla úspěšně vytvořena nebo aktualizována.");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Chyba při migraci databáze: {ex.Message}");
					}
				}
			}

			return services;
		}
	}
}
