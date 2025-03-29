using TimeTracker.BE.Web.Shared.Infrastructure;

namespace TimeTracker.Web.Blazor.Server.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerWebBlazorServer(this IServiceCollection services)
		{
			services.AddTimeTrackerBeWebShared();
			return services;
		}
	}
}
