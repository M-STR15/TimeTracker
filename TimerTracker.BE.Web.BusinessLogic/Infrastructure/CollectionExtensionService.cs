using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimerTracker.BE.Web.BusinessLogic.MappingProfiles;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.Web.Shared.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimerTrackerBeWebSharedBusinessLogic(this IServiceCollection services, string connectionString)
		{
			// Načtení connection stringu z appsettings.json
			//services.AddDbContextFactory<MainDatacontext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
			//services.AddDbContext<MainDatacontext>(options => options.UseSqlServer(connectionString));

			services.AddDbContext<MsSqlDbContext>(options => options.UseSqlServer(connectionString)
				.EnableSensitiveDataLogging()
				.LogTo(Console.WriteLine), ServiceLifetime.Scoped);

			services.AddAutoMapper(typeof(MappingProfile));
			return services;
		}
	}
}
