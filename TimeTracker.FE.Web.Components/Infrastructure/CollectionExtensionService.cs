using Microsoft.Extensions.DependencyInjection;
using TimeTracker.FE.Web.Components.Interops;
using TimeTracker.FE.Web.Components.Services;

namespace TimeTracker.FE.Web.Components.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerFeComponents(this IServiceCollection services)
		{
			services.AddScoped<ToastNotificationService>();
			services.AddScoped<FocusJsInterop>();
			services.AddScoped<ChartJsInterop>();
			services.AddScoped<LottieLoaderInterop>();

			return services;
		}
	}
}
