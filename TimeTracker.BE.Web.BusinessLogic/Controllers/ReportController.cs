using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models.Responses;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.DB.Repositories.Models.Reports;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.Web.Shared.Services;
using TimeTracker.PC.Services;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	#region ReportController
	/// <summary>
	/// Řídící třída pro zpracování reportů.
	/// </summary>
	[ApiExplorerSettings(GroupName = "v1")]
	public class ReportController : aControllerBase
	{
		private readonly ReportRepository<MsSqlDbContext> _reportRepository;
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;

		/// <summary>
		/// Konstruktor třídy ReportController.
		/// </summary>
		/// <param name="mapper">Instance mapperu pro mapování objektů.</param>
		/// <param name="reportRepository">Repozitář pro práci s reporty.</param>
		/// <param name="recordRepository">Repozitář pro práci se záznamy aktivit.</param>
		/// <param name="eventLogService">Služba pro logování událostí.</param>
		public ReportController(IMapper mapper, ReportRepository<MsSqlDbContext> reportRepository, RecordRepository<MsSqlDbContext> recordRepository, IEventLogService eventLogService)
			: base(mapper, eventLogService)
		{
			_reportRepository = reportRepository;
			_recordRepository = recordRepository;
		}

		/// <summary>
		/// Získá podrobnosti o všech záznamech aktivit.
		/// </summary>
		/// <returns>Seznam podrobností o záznamech aktivit.</returns>
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
		/// <param name="dateFrom">Počáteční datum ve formátu ISO 8601, např. 2025-04-06.</param>
		/// <param name="dateTo">Koncové datum ve formátu ISO 8601, např. 2025-04-06.</param>
		/// <returns>Seznam podrobností o záznamech aktivit.</returns>
		[HttpGet("api/v1/reports/record-activiries/{dateFrom}/{dateTo}")]
		public async Task<ActionResult<List<RecordActivityDetailDto>>> GetRecordActivitiesDetailAsync([FromRoute] DateTime dateFrom, [FromRoute] DateTime dateTo)
		{
			try
			{
				var recordActivities = await _recordRepository.GetAsync(dateFrom, dateTo);
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

		/// <summary>
		/// Získá aktivity (práce a pauzy) pro každý den v zadaném rozsahu dat.
		/// </summary>
		/// <param name="dateFrom">Počáteční datum ve formátu ISO 8601.</param>
		/// <param name="dateTo">Koncové datum ve formátu ISO 8601.</param>
		/// <returns>Seznam aktivit pro jednotlivé dny.</returns>
		[HttpGet("api/v1/reports/activity-over-days/{dateFrom}/{dateTo}")]
		public async Task<ActionResult<IEnumerable<SumInDay>>> GetActivityOverDaysAsync([FromRoute] DateTime dateFrom, [FromRoute] DateTime dateTo)
		{
			try
			{
				var list = _reportRepository.GetActivityOverDays(dateFrom, dateTo);
				if (list != null)
					return list != null ? Ok(list) : Problem();
				else
					return NotFound();
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("520a0d68-7b0a-4bef-ae9f-beab1e300ef4"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion GET
	}
}
