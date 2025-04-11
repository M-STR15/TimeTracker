using Microsoft.Extensions.DependencyInjection;
using TimeTracker.FE.Components.Services;

namespace TimeTracker.FE.Components.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerFeComponents(this IServiceCollection services)
		{
			services.AddScoped<ToastNotificationService>();
			services.AddScoped<FocusJsInterop>();

			return services;
		}
	}
}
