using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.BE.DB.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerBeDd<T>(this IServiceCollection services) where T : MainDatacontext
		{
			services.AddScoped<RecordRepository<T>>();
			services.AddScoped<ShiftRepository<T>>();

			services.AddScoped<ActivityRepository<T>>();
			services.AddScoped<ProjectRepository<T>>();
			services.AddScoped<SubModuleRepository<T>>();
			services.AddScoped<ReportRepository<T>>();


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
					kernel.Bind(service.ServiceType).ToSelf().InSingletonScope();
				}
				else
				{
					switch (service.Lifetime)
					{
						case ServiceLifetime.Singleton:
							kernel.Bind(service.ServiceType).To(service.ImplementationType).InSingletonScope();
							break;

						case ServiceLifetime.Scoped:
							kernel.Bind(service.ServiceType).To(service.ImplementationType).InScope(ctx => ctx.Kernel);
							break;

						case ServiceLifetime.Transient:
							kernel.Bind(service.ServiceType).To(service.ImplementationType).InTransientScope();
							break;
					}

					// Přidání správné vazby pro Func<T>
					if (service.ServiceType.IsGenericType && service.ServiceType.GetGenericTypeDefinition() == typeof(Func<>))
					{
						// Zajistíme, aby funkce správně vrátila instanci T
						kernel.Bind(service.ServiceType)
							  .ToMethod(ctx => new Func<object>(() => ctx.Kernel.Get(service.ServiceType.GenericTypeArguments[0])));
					}
				}
			}
		}


	}
}
