using Microsoft.Extensions.DependencyInjection;
using TimerTracker.BE.Web.BusinessLogic.Helpers;

namespace TimerTracker.BE.Web.BusinessLogic.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimerTrackerBeWebBusinessLogicService(this IServiceCollection services)
		{
			//services.AddCbDataBeDbServices();

			//services.AddMemoryCache();
			services.AddAutoMapper(typeof(MappingProfile));

			//services.AddSingleton<ProductController>();

			return services;
		}
	}
}
