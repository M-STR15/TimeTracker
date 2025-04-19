using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Tests.Web.IntegrationTests.Factories;

namespace TimeTracker.Tests.Web.IntegrationTests
{
	public class ProjectControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;
		private readonly CustomWebApplicationFactory<Program> _factory;

		public ProjectControllerTest(CustomWebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		[Fact]
		public async Task Get_ReturnsProjects()
		{
			try
			{
				var urlApi = "/api/v1/projects";
				var response = await _client.GetAsync(urlApi);
				response.EnsureSuccessStatusCode();

				var stringResponse = await response.Content.ReadAsStringAsync();
				var projects = JsonSerializer.Deserialize<List<ProjectBaseDto>>(stringResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				Assert.NotNull(projects);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}
	}
}
