using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Tests.Web.IntegrationTests.Factories;
using TimeTracker.Tests.Web.IntegrationTests.Helpers;

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
				// Assert
				Assert.NotNull(projects);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task GetProjectsAsync_ReturnsOkWithProjects()
		{
			try
			{
				// Act
				var urlApi = "/api/v1/projects";
				var response = await _client.GetAsync(urlApi);

				// Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				var projects = await response.Content.ReadFromJsonAsync<List<ProjectBaseDto>>();
				Assert.NotNull(projects);
				Assert.True(projects.Count > 0);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task AddProjectsAsync_ReturnsOkWithCreatedProject()
		{
			try
			{
				// Arrange
				var urlApi = "/api/v1/project";
				var newProject = new ProjectInsertDto
				{
					Name = MRandom.RandomString(10),
					Description = "Test Description"
				};

				// Act
				var response = await _client.PostAsJsonAsync(urlApi, newProject);

				// Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				var createdProject = await response.Content.ReadFromJsonAsync<ProjectBaseDto>();
				Assert.NotNull(createdProject);
				Assert.Equal(newProject.Name, createdProject.Name);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task UpdateProjectsAsync_ReturnsOkWithUpdatedProject()
		{
			try
			{
				var urlApi = "/api/v1/project";
				var newProject = new ProjectInsertDto
				{
					Name = MRandom.RandomString(10),
					Description = "Test Description"
				};

				// Act
				var responsePost = await _client.PostAsJsonAsync(urlApi, newProject);
				Assert.Equal(HttpStatusCode.OK, responsePost.StatusCode);
				var project = (await responsePost.Content.ReadFromJsonAsync<ProjectBaseDto>());
				// Arrange
				var updatedProject = new ProjectBaseDto
				{
					Id = project.Id,
					Name = MRandom.RandomString(10),
					Description = "Updated Description"
				};

				// Act
				var response = await _client.PutAsJsonAsync(urlApi, updatedProject);

				// Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				var resultProject = await response.Content.ReadFromJsonAsync<ProjectBaseDto>();
				Assert.NotNull(resultProject);
				Assert.Equal(updatedProject.Name, resultProject.Name);
				Assert.Equal(updatedProject.Description, "Updated Description");
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}

		[Fact]
		public async Task DeleteProjectsAsync_ReturnsOk()
		{
			try
			{
				// Arrange
				var postUrlApi = "/api/v1/project";
				var newProject = new ProjectInsertDto
				{
					Name = MRandom.RandomString(10),
					Description = "Test Description"
				};

				// Act
				var responsePost = await _client.PostAsJsonAsync(postUrlApi, newProject);
				Assert.Equal(HttpStatusCode.OK, responsePost.StatusCode);
				// Arrange

				var projectId = (await responsePost.Content.ReadFromJsonAsync<ProjectBaseDto>()).Id;
				var delUrlApi = $"/api/v1/project/{projectId}";

				// Act
				var responseDelete = await _client.DeleteAsync(delUrlApi);

				// Assert
				Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
			}
			catch (Exception ex)
			{
				Assert.True(false, $"Test měl uspět, ale došlo k výjimce: {ex.Message}");
			}
		}
	}
}
