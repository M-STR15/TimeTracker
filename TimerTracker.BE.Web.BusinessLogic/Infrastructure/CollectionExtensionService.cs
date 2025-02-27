using Microsoft.Extensions.DependencyInjection;
using TimerTracker.BE.Web.BusinessLogic.Helpers;

namespace TimeTracker.BE.Web.Shared.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimerTrackerBeWebSharedBusinessLogicService(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
			return services;
		}
	}
}
