using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	[ApiExplorerSettings(GroupName = "v1")]
	public class ShiftController : aControllerBase
	{
		private readonly ShiftRepository<MsSqlDbContext> _shiftRepository;
		public ShiftController(ShiftRepository<MsSqlDbContext> shiftRepository, IMapper mapper) : base(mapper)
		{
			_shiftRepository = shiftRepository;
		}

		/// <summary>
		/// Vrátí všechny typy směn
		/// </summary>
		/// <returns></returns>
		[HttpGet("api/v1/shifts/types")]
		public async Task<ActionResult<List<TypeShiftBaseDto>>> GetShiftTypesAsync()
		{
			try
			{
				var shiftTypes = await _shiftRepository.GetTypeShiftsAsync();
				if (shiftTypes != null)
				{
					var shiftTypesDto = _mapper.Map<List<TypeShiftBaseDto>>(shiftTypes);
					return shiftTypesDto != null ? Ok(shiftTypesDto) : Problem();
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

		[HttpGet("api/v1/shifts")]
		public async Task<ActionResult<List<ShiftBaseDto>>> GetShiftsAsync()
		{
			try
			{
				var shifts = await _shiftRepository.GetShiftsAsync();
				if (shifts != null)
				{
					var shiftsDto = _mapper.Map<List<ShiftBaseDto>>(shifts);
					return shiftsDto != null ? Ok(shiftsDto) : Problem();
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
		/// Kontrole slouží k přesnosu směn do DB, pokud nebude nějaký den na seznamu bude smazán z databáze.
		/// Vždy je potřeba zadat celý měsíc jinak budou ostatní dny smazány.
		/// </summary>
		/// <param name="shiftsDto"></param>
		/// <returns></returns>
		[HttpPut("api/v1/shifts")]
		public async Task<ActionResult<List<ShiftBaseDto>>> PutShiftsAsync([FromBody] List<ShiftBaseDto> shiftsDto)
		{
			try
			{
				if (shiftsDto != null)
				{
					var shifts = _mapper.Map<List<Shift>>(shiftsDto);
					var result = _shiftRepository.SaveShifts(shifts);
					return result ? Ok(shiftsDto) : Problem();
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
