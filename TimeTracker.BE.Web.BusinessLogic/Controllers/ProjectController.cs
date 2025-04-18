using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Entities;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.Web.Shared.Services;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProjectController : aControllerBase
	{
		protected readonly ProjectRepository<MsSqlDbContext> _projectRepository;
		protected readonly SubModuleRepository<MsSqlDbContext> _subModuleRepository;

		/// <summary>
		/// Konstruktor kontroleru pro práci s projekty a podmoduly.
		/// </summary>
		/// <param name="projectRepository">Repozitář pro práci s projekty.</param>
		/// <param name="subModuleRepository">Repozitář pro práci s podmoduly.</param>
		/// <param name="mapper">Mapper pro mapování objektů.</param>
		/// <param name="eventLogService">Služba pro logování událostí.</param>
		public ProjectController(ProjectRepository<MsSqlDbContext> projectRepository, SubModuleRepository<MsSqlDbContext> subModuleRepository, IMapper mapper, IEventLogService eventLogService) : base(mapper, eventLogService)
		{
			_projectRepository = projectRepository;
			_subModuleRepository = subModuleRepository;
		}

		#region GET
		/// <summary>
		/// Vrátí všechny projekty.
		/// </summary>
		/// <returns>Seznam projektů.</returns>
		[HttpGet("api/v1/projects")]
		public async Task<ActionResult<List<ProjectBaseDto>>> GetProjectsAsync()
		{
			try
			{
				var projects = await _projectRepository.GetAllAsync();
				if (projects != null)
				{
					var projectsDto = _mapper.Map<List<ProjectBaseDto>>(projects);
					return projects != null ? Ok(projectsDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("45d8b704-b0d1-479c-b96a-ac812703fe13"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Vrátí všechny podmoduly.
		/// </summary>
		/// <returns>Seznam podmodulů.</returns>
		[HttpGet("api/v1/projects/submodules")]
		public async Task<ActionResult<List<SubModuleBaseDto>>> GetSubModulesAsync()
		{
			try
			{
				var subModules = await _subModuleRepository.GetAllAsync();
				if (subModules != null)
				{
					var subModulesDto = _mapper.Map<List<SubModuleBaseDto>>(subModules);
					return subModulesDto != null ? Ok(subModulesDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("926ba106-8798-4a51-97b2-49074d8c3f5b"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Vrátí podmoduly podle ID projektu.
		/// </summary>
		/// <param name="projectId">ID projektu.</param>
		/// <returns>Seznam podmodulů.</returns>
		[HttpGet("api/v1/projects/submodules/{projectId}")]
		public async Task<ActionResult<List<SubModuleBaseDto>>> GetSubModulesAsync(int projectId)
		{
			try
			{
				var subModules = await _subModuleRepository.GetForTheProjectAsync(projectId);
				if (subModules != null)
				{
					var subModulesDto = _mapper.Map<List<SubModuleBaseDto>>(subModules);
					return subModulesDto != null ? Ok(subModulesDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("5c5b560f-9f2d-4af6-8db2-4042f265210e"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion GET

		#region POST
		/// <summary>
		/// Přidá nový projekt.
		/// </summary>
		/// <param name="projectBaseDto">Data projektu k přidání.</param>
		/// <returns>Přidaný projekt.</returns>
		[HttpPost("api/v1/project")]
		public async Task<ActionResult<ProjectBaseDto>> AddProjectsAsync([FromBody] ProjectInsertDto projectBaseDto)
		{
			try
			{
				var project = _mapper.Map<Project>(projectBaseDto);
				var result = await _projectRepository.SaveAsync(project);
				projectBaseDto = _mapper.Map<ProjectBaseDto>(result);
				return result != null ? Ok(projectBaseDto) : Problem();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("afce10f7-12a0-45d5-9f1a-a47b8d91856e"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Přidá nový podmodul.
		/// </summary>
		/// <param name="subModuleBaseDto">Data podmodulu k přidání.</param>
		/// <returns>Přidaný podmodul.</returns>
		[HttpPost("api/v1/projects/submodule")]
		public async Task<ActionResult<SubModuleBaseDto>> AddSubModulesAsync([FromBody] SubModuleBaseDto subModuleBaseDto)
		{
			try
			{
				var subModule = _mapper.Map<SubModuleBaseDto>(subModuleBaseDto);
				var result = await _subModuleRepository.SaveAsync(subModule);
				subModuleBaseDto = _mapper.Map<SubModuleBaseDto>(result);
				return result != null ? Ok(subModule) : Problem();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("71c4af17-9b5c-4ccc-a474-d5efcd8fb188"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion POST

		#region PUT
		/// <summary>
		/// Aktualizuje existující projekt.
		/// </summary>
		/// <param name="projectBaseDto">Data projektu k aktualizaci.</param>
		/// <returns>Aktualizovaný projekt.</returns>
		[HttpPut("api/v1/project")]
		public async Task<ActionResult<ProjectBaseDto>> UpdateProjectsAsync([FromBody] ProjectBaseDto projectBaseDto)
		{
			try
			{
				var project = _mapper.Map<Project>(projectBaseDto);
				var result = await _projectRepository.SaveAsync(project);
				projectBaseDto = _mapper.Map<ProjectBaseDto>(result);
				return result != null ? Ok(projectBaseDto) : Problem();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("fd877d7c-3181-40f9-b849-31fe089608d2"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Aktualizuje existující podmodul.
		/// </summary>
		/// <param name="subModuleBaseDto">Data podmodulu k aktualizaci.</param>
		/// <returns>Aktualizovaný podmodul.</returns>
		[HttpPut("api/v1/projects/submodule")]
		public async Task<ActionResult<SubModuleBaseDto>> PutSubModulesAsync([FromBody] SubModuleBaseDto subModuleBaseDto)
		{
			try
			{
				var subModule = _mapper.Map<SubModuleBaseDto>(subModuleBaseDto);
				var result = await _subModuleRepository.SaveAsync(subModule);
				subModuleBaseDto = _mapper.Map<SubModuleBaseDto>(result);
				return result != null ? Ok(subModule) : Problem();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("0a18c552-64c1-446e-96ce-3104b2cdae57"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion PUT

		#region DELETE
		/// <summary>
		/// Smaže projekt podle ID.
		/// </summary>
		/// <param name="projectId">ID projektu k odstranění.</param>
		/// <returns>Výsledek operace.</returns>
		[HttpDelete("api/v1/project/{projectId}")]
		public async Task<IActionResult> DeleteProjectsAsync(int projectId)
		{
			try
			{
				var result = await _projectRepository.DeleteAsync(projectId);
				return result ? Ok() : Problem();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("6e3750ab-5e5f-4736-ad17-de95590d7dfc"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Smaže podmodul podle ID.
		/// </summary>
		/// <param name="submoduleId">ID podmodulu k odstranění.</param>
		/// <returns>Výsledek operace.</returns>
		[HttpDelete("api/v1/projects/submodule/{submoduleId}")]
		public async Task<IActionResult> DeleteSubModulesAsync(int submoduleId)
		{
			try
			{
				var result = await _subModuleRepository.DeleteAsync(submoduleId);
				return result ? Ok() : Problem();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("9040f36f-e90f-4dc6-9211-f28f2bb2c25c"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion DELETE
	}
}
