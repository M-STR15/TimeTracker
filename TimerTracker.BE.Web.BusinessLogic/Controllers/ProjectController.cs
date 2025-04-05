using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	[SwaggerResponse(200, "Úspěšné získání položky/položek [Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200)")]
	[SwaggerResponse(404, "Položka/Položky nenalezeny.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404)")]
	[SwaggerResponse(500, "Chyba serveru.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500)")]
	public class ProjectController : ControllerBase
	{
		private readonly ProjectRepository<MsSqlDbContext> _projectRepository;
		private readonly IMapper _mapper;
		public ProjectController(ProjectRepository<MsSqlDbContext> projectRepository, IMapper mapper)
		{
			_projectRepository = projectRepository;
			_mapper = mapper;
		}
		#region GET
		/// <summary>
		/// Vrátí všechny projekty
		/// </summary>
		/// <returns></returns>
		[HttpGet("api/v1/projects")]
		public async Task<ActionResult<List<ProjectBaseDto>>> GetProjectsAsync()
		{
			try
			{
				var projects = await _projectRepository.GetProjectsAsync();
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
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("api/v1/projects/submodules")]
		public async Task<ActionResult<List<SubModuleBaseDto>>> GetSubModulesAsync()
		{
			try
			{
				var subModules = await _projectRepository.GetSubModulesAsync();
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
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("api/v1/projects/submodules/{projectId}")]
		public async Task<ActionResult<List<SubModuleBaseDto>>> GetSubModulesAsync(int projectId)
		{
			try
			{
				var subModules = await _projectRepository.GetSubModulesAsync(projectId);
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
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion GET
		#region POST
		[HttpPost("api/v1/project")]
		public async Task<ActionResult<ProjectBaseDto>> AddProjectsAsync([FromBody] ProjectInsertDto projectBaseDto)
		{
			try
			{
				var project = _mapper.Map<Project>(projectBaseDto);
				var result = await _projectRepository.SaveProjectAsync(project);
				projectBaseDto = _mapper.Map<ProjectBaseDto>(result);
				return result != null ? Ok(projectBaseDto) : Problem();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost("api/v1/projects/submodule")]
		public async Task<ActionResult<SubModuleBaseDto>> AddSubModulesAsync([FromBody] SubModuleBaseDto subModuleBaseDto)
		{
			try
			{
				var subModule = _mapper.Map<SubModuleBaseDto>(subModuleBaseDto);
				var result = await _projectRepository.SaveSubModuleAsync(subModule);
				subModuleBaseDto = _mapper.Map<SubModuleBaseDto>(result);
				return result != null ? Ok(subModule) : Problem();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion POST
		#region PUT
		[HttpPut("api/v1/project")]
		public async Task<ActionResult<ProjectBaseDto>> UpdateProjectsAsync([FromBody] ProjectBaseDto projectBaseDto)
		{
			try
			{
				var project = _mapper.Map<Project>(projectBaseDto);
				var result = await _projectRepository.SaveProjectAsync(project);
				projectBaseDto = _mapper.Map<ProjectBaseDto>(result);
				return result != null ? Ok(projectBaseDto) : Problem();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut("api/v1/projects/submodule")]
		public async Task<ActionResult<SubModuleBaseDto>> PutSubModulesAsync([FromBody] SubModuleBaseDto subModuleBaseDto)
		{
			try
			{
				var subModule = _mapper.Map<SubModuleBaseDto>(subModuleBaseDto);
				var result = await _projectRepository.SaveSubModuleAsync(subModule);
				subModuleBaseDto = _mapper.Map<SubModuleBaseDto>(result);
				return result != null ? Ok(subModule) : Problem();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion PUT
		#region DELETE
		[HttpDelete("api/v1/project/{projectId}")]
		public async Task<IActionResult> DeleteProjectsAsync(int projectId)
		{
			try
			{
				var result = await _projectRepository.DeleteProjectAsync(projectId);
				return result ? Ok() : Problem();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("api/v1/projects/submodule/{submoduleId}")]
		public async Task<IActionResult> DeleteSubModulesAsync(int submoduleId)
		{
			try
			{
				var result = await _projectRepository.DeleteSubModuleAsync(submoduleId);
				return result ? Ok() : Problem();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion DELETE
	}
}
