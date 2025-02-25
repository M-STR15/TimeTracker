using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.DB.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeDdService(this IServiceCollection services)
		{
			//_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();
			//services.AddCbDataBeDbServices();

			////services.AddMemoryCache();
			//services.AddAutoMapper(typeof(MappingProfile));

			services.AddSingleton<MainDatacontext>();

			return services;
		}

		public static void AddToNinject(this IServiceCollection services, IKernel kernel)
		{
			foreach (var service in services)
			{
				kernel.Bind(service.ServiceType).To(service.ImplementationType).InSingletonScope();
			}
		}
	}
}
