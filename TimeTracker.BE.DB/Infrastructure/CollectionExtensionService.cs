using Microsoft.Extensions.DependencyInjection;

namespace TimeTracker.BE.DB.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeDd(this IServiceCollection services)
		{
			return services;
		}
	}
}
