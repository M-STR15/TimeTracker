using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.Components.Pages
{
	public partial class SettingPage
	{
		private List<TableColumnDefinition<ProjectBaseDto>>? _projectColumns;
		private List<ProjectBaseDto>? _projects;
		private ProjectBaseDto? _selectedProject;
		private SubModuleBaseDto? _selectedSubModule;
		private List<TableColumnDefinition<SubModuleBaseDto>>? _subModuleColumns;
		private List<SubModuleBaseDto>? _subModules;
		private bool IsOpenAddOrEditSubModuleModal { get; set; } = false;
		private bool IsOpenAddOrEdtiProjectModal { get; set; } = false;
		private bool IsOpenProductDeleteQueryModal { get; set; } = false;
		private bool IsOpenSubModuleDeleteQueryModal { get; set; } = false;

		public static List<TableColumnDefinition<ProjectBaseDto>> CreateTableColumnDefinition_Project()
		{
			return new List<TableColumnDefinition<ProjectBaseDto>>
				{
					new TableColumnDefinition<ProjectBaseDto>
					{
						Header = "Name",
						Key = nameof(ProjectBaseDto.Name),
						Width=150,
						eTypeColumn=eTypeColumn.Text
					},
				};
		}

		public static List<TableColumnDefinition<SubModuleBaseDto>> CreateTableColumnDefinition_SubModuleBaseDto()
		{
			return new List<TableColumnDefinition<SubModuleBaseDto>>
				{
					new TableColumnDefinition<SubModuleBaseDto>
					{
						Header = "Name",
						Key = nameof(ProjectBaseDto.Name),
						Width=150,
						eTypeColumn=eTypeColumn.Text
					},
				};
		}

		protected async override Task OnInitializedAsync()
		{

			_projectColumns = CreateTableColumnDefinition_Project();
			_subModuleColumns = CreateTableColumnDefinition_SubModuleBaseDto();

			await loadProjectListAsync();

			await base.OnInitializedAsync();
		}

		private async Task processingDeleteProjectResult(bool confirmed)
		{
			if (confirmed && _selectedProject != null)
			{
				var urlApi = $"/api/v1/project/{_selectedProject.Id}";
				await _httpClient.DeleteAsync(urlApi);
				_selectedProject = null;
				await loadProjectListAsync();
			}
		}

		private async Task processingDeleteSubModuleResult(bool confirmed)
		{
			if (confirmed && _selectedSubModule != null)
			{
				var urlApi = $"/api/v1/projects/submodule/{_selectedSubModule.Id}";
				await _httpClient.DeleteAsync(urlApi);
				_selectedSubModule = null;
				await loadSubModuleListAsync();
			}
		}

		private async Task loadProjectListAsync()
		{
			var urlApi = $"/api/v1/projects";
			var projects = await _httpClient.GetFromJsonAsync<List<ProjectBaseDto>>(urlApi);
			if (projects != null)
				_projects = projects;

			base.StateHasChanged();
		}

		private async Task loadSubModuleListAsync()
		{
			if (_selectedProject != null)
			{
				var urlApi = $"/api/v1/projects/submodules/{_selectedProject.Id}";
				Console.WriteLine($"Načítám z URL: {urlApi}");

				try
				{
					var response = await _httpClient.GetAsync(urlApi);

					if (response.IsSuccessStatusCode)
					{
						var subModules = await response.Content.ReadFromJsonAsync<List<SubModuleBaseDto>>();
						if (subModules != null)
							_subModules = subModules;
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
					{
						Console.WriteLine("Submoduly nenalezeny (404).");
						_subModules = new(); // nebo ponechat null
					}
					else
					{
						Console.WriteLine($"Chyba při načítání: {response.StatusCode}");
					}
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine($"Chyba HTTP: {ex.Message}");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Obecná chyba: {ex.Message}");
				}

				base.StateHasChanged();
			}
		}

		private async void onAfterCloseModalAddOrEditProjectChanged() => await loadProjectListAsync();
		private async void onAfterCloseModalAddOrEditSubModuleChanged() => await loadSubModuleListAsync();

		// Handle row selection for projects
		private async void onProjectRowSelected(ProjectBaseDto selectedRow)
		{
			if (!selectedRow.Equals(_selectedProject))
			{
				_selectedProject = selectedRow;
				await loadSubModuleListAsync();
				base.StateHasChanged();
			}
		}

		// Handle row selection for submodules
		private void onSubModuleRowSelected(SubModuleBaseDto selectedRow)
		{
			if (!selectedRow.Equals(_selectedSubModule))
			{
				_selectedSubModule = selectedRow;
				base.StateHasChanged();
			}
		}

		private void showModalAddProject()
		{
			_selectedProject = new();
			IsOpenAddOrEdtiProjectModal = true;
		}
		private void showModalAddSubmodule()
		{
			if (_selectedProject != null)
			{
				_selectedSubModule = new();
				_selectedSubModule.ProjectId = _selectedProject.Id;
				IsOpenAddOrEditSubModuleModal = true;
			}
		}

		private void showModalDeleteProject() => IsOpenProductDeleteQueryModal = true;

		private void showModalDeleteSubmodule() => IsOpenSubModuleDeleteQueryModal = true;

		private void showModalEditProject() => IsOpenAddOrEdtiProjectModal = true;

		private void showModalEditSubmodule()
		{

			IsOpenAddOrEditSubModuleModal = true;
		}
	}
}
