using TimeTracker.BE.Web.Shared.Infrastructure;
using TimeTracker.Web.Blazor.Server.MappingProfiles;
using TimeTracker.Web.Blazor.Server.Services;

namespace TimeTracker.Web.Blazor.Server.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerWebBlazorServer(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
			services.AddTimeTrackerBeWebShared();
			services.AddScoped<ToastNotificationService>();

			return services;
		}
	}
}
