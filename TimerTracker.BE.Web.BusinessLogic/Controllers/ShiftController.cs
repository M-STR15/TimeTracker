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
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	[SwaggerResponse(200, "Úspěšné získání položky/položek [Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200)")]
	[SwaggerResponse(404, "Položka/Položky nenalezeny.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404)")]
	[SwaggerResponse(500, "Chyba serveru.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500)")]
	public class ShiftController : ControllerBase
	{
		private readonly ShiftRepository<MsSqlDbContext> _shiftRepository;
		private readonly IMapper _mapper;
		public ShiftController(ShiftRepository<MsSqlDbContext> shiftRepository, IMapper mapper)
		{
			_shiftRepository = shiftRepository;
			_mapper = mapper;
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
		public async Task<ActionResult<List<ShiftBaseDto>>> PutShiftsAsync(List<ShiftBaseDto> shiftsDto)
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
