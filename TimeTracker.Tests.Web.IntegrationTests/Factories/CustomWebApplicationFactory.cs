using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
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
				// Odeberte registraci existujícího DbContextu
				//var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<InMemoryDbContext>));

				//if (descriptor != null)
				//	services.Remove(descriptor);

				// Přidejte InMemory databázi pro testování
				//services.AddDbContext<MsSqlDbContext>(options =>
				//	options.UseInMemoryDatabase("TestDb"));

				// Přidejte logiku pro business logic (bez znovu registrace DbContextu)
				//services.AddTimeTrackerBeWebSharedBusinessLogic<InMemoryDbContext>();

				// Inicializace databáze pro testy
				using var scope = services.BuildServiceProvider().CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
				db.Database.EnsureCreated(); // Ujistěte se, že databáze je vytvořena
			});
		}
	}
}
