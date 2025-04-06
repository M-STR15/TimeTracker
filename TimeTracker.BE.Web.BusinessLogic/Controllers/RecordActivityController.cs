using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{

	[ApiExplorerSettings(GroupName = "v1")]
	public class RecordActivityController : aControllerBase
	{
		private readonly ActivityRepository<MsSqlDbContext> _activityRepository;
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;
		public RecordActivityController(ActivityRepository<MsSqlDbContext> activityRepository, RecordRepository<MsSqlDbContext> recordRepository, IMapper mapper) : base(mapper)
		{
			_activityRepository = activityRepository;
			_recordRepository = recordRepository;
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
		public async Task<ActionResult<List<RecordActivityInsertDto>>> AddRecordActivitiesAsync([FromBody] RecordActivityInsertDto recordActivityInsertDto)
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
