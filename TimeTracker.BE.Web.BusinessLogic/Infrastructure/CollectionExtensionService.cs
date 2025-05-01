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
		private const string _connectionString = @"Server=DESKTOP-JS0N1LD\SQLEXPRESS;Integrated Security=true;TrustServerCertificate=true;Database=TimeTrackerDB";

		public static IServiceCollection AddTimeTrackerBeWebSharedBusinessLogic<T>(
		this IServiceCollection services) // Parametr pro volbu mezi InMemory a MS SQL Server
			where T : MainDatacontext
		{
			if (typeof(T) == typeof(InMemoryDbContext))
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
					options.UseSqlServer(_connectionString);
					//.EnableSensitiveDataLogging()
					//.LogTo(Console.WriteLine);

				}, ServiceLifetime.Scoped);
			}


			services.AddScoped<Func<T>>(provider => () => provider.GetRequiredService<T>());

			services.AddTimeTrackerBeDd<T>();

			//services.AddScoped<ProjectController>();
			//services.AddScoped<ShiftController>();
			services.AddAutoMapper(typeof(MappingProfile));

			// Automatické vytvoření databáze
			using (var serviceProvider = services.BuildServiceProvider())
			using (var scope = serviceProvider.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<T>();
				try
				{
					if (typeof(T) == typeof(InMemoryDbContext))
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