using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.Web.BusinessLogic.Infrastructure;

namespace TimeTracker.Tests.Web.IntegrationTests.Factories
{
	public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseEnvironment("Testing"); // Nastavení prostředí pro testy

			builder.ConfigureServices(services =>
			{
				services.AddTimeTrackerBeWebSharedBusinessLogic<InMemoryDbContext>(useInMemoryDatabase: true);

				// Inicializace databáze pro testy
				using var scope = services.BuildServiceProvider().CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<InMemoryDbContext>();
				db.Database.EnsureCreated(); // Ujistěte se, že databáze je vytvořena
			});
		}
	}
}
