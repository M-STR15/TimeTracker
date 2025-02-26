using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.Configuration;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.DB.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeDdService(this IServiceCollection services, IConfiguration configuration, DbContextOptions<MainDatacontext> options)
		{
			//_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();
			//services.AddCbDataBeDbServices();

			////services.AddMemoryCache();
			//services.AddAutoMapper(typeof(MappingProfile));

			services.AddSingleton<IConfiguration>(configuration);
			services.AddSingleton<DbContextOptions<MainDatacontext>>(options);
			services.AddSingleton<MainDatacontext>();

			return services;
		}

		public static void AddToNinject(this IServiceCollection services, IKernel kernel)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (kernel == null) throw new ArgumentNullException(nameof(kernel));

			foreach (var service in services)
			{
				if (service.ServiceType == null)
				{
					throw new InvalidOperationException("ServiceType cannot be null.");
				}

				if (service.ImplementationType == null)
				{
					// Pokud není implementace definovaná, použij ToSelf()
					kernel.Bind(service.ServiceType).ToSelf().InSingletonScope();
				}
				else
				{
					kernel.Bind(service.ServiceType).To(service.ImplementationType).InSingletonScope();
				}
			}
		}
	}
}
