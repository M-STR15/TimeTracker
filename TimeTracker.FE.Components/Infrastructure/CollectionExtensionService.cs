using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.FE.Components.Services;

namespace TimeTracker.FE.Components.Infrastructure
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddTimeTrackerFeComponents(this IServiceCollection services)
		{
			services.AddScoped<ToastNotificationService>();

			return services;
		}
	}
}
