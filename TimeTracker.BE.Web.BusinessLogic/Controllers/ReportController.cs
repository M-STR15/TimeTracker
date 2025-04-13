using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.Web.Shared.Services;
using TimeTracker.PC.Services;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	[ApiExplorerSettings(GroupName = "v1")]
	public class ReportController : aControllerBase
	{
		private readonly ReportRepository<MsSqlDbContext> _reportRepository;
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;
		public ReportController(IMapper mapper, ReportRepository<MsSqlDbContext> reportRepository, RecordRepository<MsSqlDbContext> recordRepository, IEventLogService eventLogService) : base(mapper, eventLogService)
		{
			_reportRepository = reportRepository;
			_recordRepository = recordRepository;
		}

		[HttpGet("api/v1/reports/record-activiries")]
		public async Task<ActionResult<List<TypeShiftBaseDto>>> GetRecordActivitiesDetailAsync()
		{
			try
			{
				var dateStart = DateTime.MinValue;
				var dateEnd = DateTime.Now;
				var recordActivities = await _recordRepository.GetAsync(dateStart, dateEnd);
				if (recordActivities != null)
				{
					var recordActivitiesDto = _mapper.Map<List<RecordActivityDetailDto>>(recordActivities);
					return recordActivitiesDto != null ? Ok(recordActivitiesDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("adb89d9d-3723-41c4-949b-f7d9b23f899f"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Získá podrobnosti o záznamech aktivit v zadaném časovém rozmezí.
		/// </summary>
		/// <param name="dateStart">Počáteční datum ve formátu ISO 8601, např. 2025-04-06.</param>
		/// <param name="dateEnd">Koncové datum ve formátu ISO 8601, např. 2025-04-06.</param>
		/// <returns>Seznam podrobností o záznamech aktivit.</returns>
		[HttpGet("api/v1/reports/record-activiries/{dateStart}/{dateEnd}")]
		public async Task<ActionResult<List<RecordActivityDetailDto>>> GetRecordActivitiesDetailAsync([FromRoute] DateTime dateStart, [FromRoute] DateTime dateEnd)
		{
			try
			{
				var recordActivities = await _recordRepository.GetAsync(dateStart, dateEnd);
				if (recordActivities != null)
				{
					var recordActivitiesDto = _mapper.Map<List<RecordActivityDetailDto>>(recordActivities);
					return recordActivitiesDto != null ? Ok(recordActivitiesDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("4f259ab6-4044-41a1-9f66-8eb1147f8132"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Získá celkové časy pro aktuální den a směnu.
		/// </summary>
		/// <returns>Objekt TotalTimesDto obsahující celkové časy.</returns>
		[HttpGet("api/v1/reports/total-times")]
		public async Task<ActionResult<TotalTimesDto>> GetTotalTimesDtoAsync()
		{
			try
			{
				var _lastRecordActivity = await _recordRepository.GetLastAsync();
				var shiftGuidId = _lastRecordActivity?.Shift?.GuidId ?? Guid.Empty;

				var calcHours_forToday_fromDb = _reportRepository.GetActualSumaryHours();
				var calcHours_forShift_fromDb = _reportRepository.GetSumaryHoursShift(shiftGuidId);

				var totalTime = TotalTimesService.Get(calcHours_forToday_fromDb, calcHours_forShift_fromDb, _lastRecordActivity);

				if (totalTime != null)
					return totalTime != null ? Ok(totalTime) : Problem();
				else
					return NotFound();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("048c4139-0645-4af9-a84e-fa90cc08c013"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}


		/// <summary>
		/// Získá pracovní hodiny na pracovišti a v režimu home office pro zadaný měsíc a rok.
		/// Vrací také plánované pracovní hodiny pro obě kategorie.
		/// </summary>
		/// <param name="year">Rok, pro který se mají získat pracovní hodiny (např. 2023).</param>
		/// <param name="month">Měsíc, pro který se mají získat pracovní hodiny (1-12).</param>
		/// <returns>Objekt WorkplaceHours obsahující seznamy pracovních hodin a plánovaných hodin.</returns>
		[HttpGet("api/v1/reports/workplace-hours/{year}/{month}")]
		public async Task<ActionResult<WorkplaceHours>> GetTotalTimesDtoAsync(int year, int month)
		{
			try
			{
				var workplaceHours = _reportRepository.GetWorkplaceHours(year, month);

				if (workplaceHours != null)
					return workplaceHours != null ? Ok(workplaceHours) : Problem();
				else
					return NotFound();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("9c7b676e-3561-4aa2-82fb-04ce7a8bcd2d"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
