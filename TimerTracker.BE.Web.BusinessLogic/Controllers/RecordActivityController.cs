using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimerTracker.BE.Web.BusinessLogic.Models.DTOs;
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
	public class RecordActivityController : ControllerBase
	{
		private readonly ActivityRepository<MsSqlDbContext> _activityRepository;
		private readonly IMapper _mapper;
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;
		public RecordActivityController(ActivityRepository<MsSqlDbContext> activityRepository, RecordRepository<MsSqlDbContext> recordRepository, IMapper mapper)
		{
			_activityRepository = activityRepository;
			_recordRepository = recordRepository;
			_mapper = mapper;
		}

		[HttpGet("api/v1/activities")]
		public async Task<ActionResult<List<ActivityBaseDto>>> GetActivitiesAsync()
		{
			try
			{
				var activities = await _activityRepository.GetActivitiesAsync();
				if (activities != null)
				{
					var activitiesDto = _mapper.Map<List<ActivityBaseDto>>(activities);
					return activitiesDto != null ? Ok(activitiesDto) : Problem();
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

		[HttpGet("api/v1/record-activities")]
		public async Task<ActionResult<List<RecordActivityBaseDto>>> GetRecordActivitiesAsync()
		{
			try
			{
				var recordActivities = await _recordRepository.GetRecordsAsync();
				if (recordActivities != null)
				{
					var recordActivitiesDto = _mapper.Map<List<RecordActivityBaseDto>>(recordActivities);
					return recordActivitiesDto != null ? Ok(recordActivitiesDto) : Problem();
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

		[HttpPost("api/v1/record-activities")]
		public async Task<ActionResult<List<RecordActivityInsertDto>>> AddRecordActivitiesAsync(RecordActivityInsertDto recordActivityInsertDto)
		{
			try
			{
				var recordActivity = _mapper.Map<RecordActivity>(recordActivityInsertDto);
				recordActivity = await _recordRepository.SaveRecordAsync(recordActivity);
				if (recordActivity != null)
				{
					var recordActivityDto = _mapper.Map<RecordActivityBaseDto>(recordActivity);
					return recordActivityDto != null ? Ok(recordActivityDto) : Problem();
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
	}
}
