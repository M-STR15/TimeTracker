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
	/// <summary>
	/// Kontroler pro správu záznamů aktivit.
	/// </summary>
	public class RecordActivityController : aControllerBase
	{
		private readonly ActivityRepository<MsSqlDbContext> _activityRepository;
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;

		/// <summary>
		/// Konstruktor kontroleru pro správu záznamů aktivit.
		/// </summary>
		/// <param name="activityRepository">Repozitář aktivit.</param>
		/// <param name="recordRepository">Repozitář záznamů aktivit.</param>
		/// <param name="mapper">Mapper pro mapování objektů.</param>
		/// <param name="eventLogService">Služba pro logování událostí.</param>
		public RecordActivityController(ActivityRepository<MsSqlDbContext> activityRepository, RecordRepository<MsSqlDbContext> recordRepository, IMapper mapper, IEventLogService eventLogService) : base(mapper, eventLogService)
		{
			_activityRepository = activityRepository;
			_recordRepository = recordRepository;
		}

		#region GET

		/// <summary>
		/// Získá seznam všech aktivit.
		/// </summary>
		/// <returns>Seznam aktivit.</returns>
		[HttpGet("api/v1/activities")]
		public async Task<ActionResult<List<ActivityBaseDto>>> GetActivitiesAsync()
		{
			try
			{
				var activities = await _activityRepository.GetAllAsync();
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
				_eventLogService.LogError(Guid.Parse("92344ec4-e18f-4cef-ab10-631c18775e67"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Získá seznam všech záznamů aktivit.
		/// </summary>
		/// <returns>Seznam záznamů aktivit.</returns>
		[HttpGet("api/v1/record-activities")]
		public async Task<ActionResult<List<RecordActivityBaseDto>>> GetRecordActivitiesAsync()
		{
			try
			{
				var recordActivities = await _recordRepository.GetAllAsync();
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
				_eventLogService.LogError(Guid.Parse("2df8ed97-a336-45f8-b939-0795846b92d2"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Získá poslední záznam aktivity.
		/// </summary>
		/// <returns>Poslední záznam aktivity.</returns>
		[HttpGet("api/v1/last-record-activity")]
		public async Task<ActionResult<RecordActivityDetailDto>> GetLastRecordActivityAsync()
		{
			try
			{
				var recordActivity = await _recordRepository.GetLastAsync();
				if (recordActivity != null)
				{
					var recordActivityDto = _mapper.Map<RecordActivityDetailDto>(recordActivity);
					return recordActivityDto != null ? Ok(recordActivityDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("c9b777fa-2481-4ca1-845c-dbcc549131a9"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion GET

		#region ADD

		/// <summary>
		/// Přidá nový záznam aktivity.
		/// </summary>
		/// <param name="recordActivityInsertDto">Data nového záznamu aktivity.</param>
		/// <returns>Vytvořený záznam aktivity.</returns>
		[HttpPost("api/v1/record-activities")]
		public async Task<ActionResult<List<RecordActivityInsertDto>>> AddRecordActivitiesAsync([FromBody] RecordActivityInsertDto recordActivityInsertDto)
		{
			try
			{
				var recordActivity = _mapper.Map<RecordActivity>(recordActivityInsertDto);
				recordActivity = await _recordRepository.SaveAsync(recordActivity);
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
				_eventLogService.LogError(Guid.Parse("77c48ffc-666e-4e91-accc-50565cfc3a72"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion ADD

		#region DELETE

		/// <summary>
		/// Odstraní záznam aktivity podle zadaného Guid.
		/// </summary>
		/// <param name="recordActivityGuidId">Globálně unikátní identifikátor záznamu aktivity.</param>
		/// <returns>Výsledek operace.</returns>
		[HttpDelete("api/v1/record-activities/{recordActivityGuidId}")]
		public async Task<ActionResult<List<RecordActivityInsertDto>>> DeleteRecordActivitiesAsync(Guid recordActivityGuidId)
		{
			try
			{
				var result = await _recordRepository.DeleteAsync(recordActivityGuidId);
				if (result != null)
				{
					return result == true ? Ok() : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("cac6b8ba-5ed2-41d2-8507-00218a8443ae"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion DELETE
	}
}
