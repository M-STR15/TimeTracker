using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimerTracker.BE.Web.BusinessLogic.Controllers;
using TimerTracker.BE.Web.BusinessLogic.MappingProfiles;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Infrastructure;

namespace TimeTracker.BE.Web.Shared.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimerTrackerBeWebSharedBusinessLogic(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<MsSqlDbContext>(options =>
				options.UseSqlServer(connectionString)
					.EnableSensitiveDataLogging()
					.LogTo(Console.WriteLine), ServiceLifetime.Scoped);


			services.AddScoped<Func<MsSqlDbContext>>(provider => () => provider.GetRequiredService<MsSqlDbContext>());

			services.AddTimeTrackerBeDd<MsSqlDbContext>();

			services.AddScoped<ProjectController>();
			// AutoMapper pro mapování
			services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}
	}
}
