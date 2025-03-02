using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;

namespace TimerTracker.BE.Web.BusinessLogic.Controllers
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
				//_eventLogService.WriteError(Guid.Parse("88512c81-4994-46c6-95c7-a3accbcafe20"), ex.Message);
				//Debug.WriteLine(ex.ToString());
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
