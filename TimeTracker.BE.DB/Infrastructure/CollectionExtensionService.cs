using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TimeTracker.BE.DB.DataAccess;

namespace TimeTracker.BE.DB.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeDd(this IServiceCollection services)
		{
			//_container.Bind<MainDatacontext>().To<MainDatacontext>().InSingletonScope();
			//services.AddCbDataBeDbServices();

			////services.AddMemoryCache();
			//services.AddAutoMapper(typeof(MappingProfile));

			//services.AddSingleton<IConfiguration>(configuration);
			//services.AddDbContext<DbContextOptions>(options);
			services.AddSingleton<MainDatacontext>();
			//services.AddScoped<Func<MainDatacontext>>(provider => () => provider.GetRequiredService<MainDatacontext>());
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
