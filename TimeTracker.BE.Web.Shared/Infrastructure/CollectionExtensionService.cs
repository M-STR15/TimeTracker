using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BE.Web.Shared.Services;

namespace TimeTracker.BE.Web.Shared.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeWebSharedService(this IServiceCollection services)
		{
			services.AddSingleton<IEventLogService, EventLogService>();
			return services;
		}
	}
}
