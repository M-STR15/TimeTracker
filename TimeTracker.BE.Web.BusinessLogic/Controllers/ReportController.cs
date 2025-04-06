using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	[ApiExplorerSettings(GroupName = "v1")]
	public class ReportController : aControllerBase
	{
		private readonly ReportRepository<MsSqlDbContext> _reportRepository;
		private readonly RecordRepository<MsSqlDbContext> _recordRepository;
		public ReportController(IMapper mapper, ReportRepository<MsSqlDbContext> reportRepository, RecordRepository<MsSqlDbContext> recordRepository) : base(mapper)
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
				var recordActivities = await _recordRepository.GetRecordsAsync(dateStart, dateEnd);
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
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dateStart">datum musí být ve formátu ISO 8601 např. 2025-04-06</param>
		/// <param name="dateEnd">datum musí být ve formátu ISO 8601 např. 2025-04-06</param>
		/// <returns></returns>
		[HttpGet("api/v1/reports/record-activiries/{dateStart}/{dateEnd}")]
		public async Task<ActionResult<List<TypeShiftBaseDto>>> GetRecordActivitiesDetailAsync([FromRoute] DateTime dateStart, [FromRoute] DateTime dateEnd)
		{
			try
			{
				var recordActivities = await _recordRepository.GetRecordsAsync(dateStart, dateEnd);
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
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
