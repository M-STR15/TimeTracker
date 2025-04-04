using TimerTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.Components.Pages
{
	public partial class SettingPage
	{
		private List<TableColumnDefinition<ProjectBaseDto>>? _projectColumns;
		private List<ProjectBaseDto> _projects;
		private ProjectBaseDto _selectedProject;
		private SubModuleBaseDto _selectedSubModule;
		private List<TableColumnDefinition<SubModuleBaseDto>>? _subModuleColumns;
		private List<SubModuleBaseDto> _subModules;
		public bool IsOpenAddOrEditSubModuleModal { get; set; } = false;
		public bool IsOpenAddOrEdtiProjectModal { get; set; } = false;
		public bool IsOpenProductDeleteQueryModal { get; set; } = false;

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

			loadProjectList();
			loadSubModuleList();

			await base.OnInitializedAsync();
		}

		private async Task handleDeleteConfirmationResult(bool confirmed)
		{
			if (confirmed)
			{
				var urlApi = $"/api/v1/project/{_selectedProject.Id}";
				await _httpClient.DeleteAsync(urlApi);
				loadProjectList();
			}
		}

		private async void loadProjectList()
		{
			var projects = await _httpClient.GetFromJsonAsync<List<ProjectBaseDto>>("/api/v1/projects");
			if (projects != null)
				_projects = projects;

			base.StateHasChanged();
		}

		private async void loadSubModuleList()
		{
			var subModules = await _httpClient.GetFromJsonAsync<List<SubModuleBaseDto>>("/api/v1/projects/submodules");
			if (subModules != null)
				_subModules = subModules;

			base.StateHasChanged();
		}

		private void onCloseModalAddPrjectChanged() => loadProjectList();

		// Handle row selection for projects
		private void onProjectRowSelected(ProjectBaseDto selectedRow)
		{
			if (selectedRow.Equals(_selectedProject))
				_selectedProject = selectedRow;
		}

		// Handle row selection for submodules
		private void onSubModuleRowSelected(SubModuleBaseDto selectedRow)
		{
			if (selectedRow.Equals(_selectedSubModule))
				_selectedSubModule = selectedRow;
		}

		private void showModalAddProject()
		{
			_selectedProject = new();
			IsOpenAddOrEdtiProjectModal = true;
		}
		private void showModalAddSubmodule()
		{
			_selectedSubModule = new();
			IsOpenAddOrEditSubModuleModal = true;

		}

		private void showModalDeleteProject() => IsOpenProductDeleteQueryModal = true;

		private void showModalDeleteSubmodule()
		{

		}

		private void showModalEditProject() => IsOpenAddOrEdtiProjectModal = true;

		private void showModalEditSubmodule() => IsOpenAddOrEditSubModuleModal = true;
	}
}
