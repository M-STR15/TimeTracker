using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.Web.Shared.Services;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	/// <summary>
	/// Řadič pro správu směn a typů směn.
	/// </summary>
	[ApiExplorerSettings(GroupName = "v1")]
	public class ShiftController : aControllerBase
	{
		private readonly ShiftRepository<MsSqlDbContext> _shiftRepository;
		private readonly TypeShiftRepository<MsSqlDbContext> _typeShiftRepository;

		/// <summary>
		/// Konstruktor řadiče ShiftController.
		/// </summary>
		/// <param name="shiftRepository">Úložiště směn.</param>
		/// <param name="typeShiftRepository">Úložiště typů směn.</param>
		/// <param name="mapper">Automapper pro mapování objektů.</param>
		/// <param name="eventLogService">Služba pro logování událostí.</param>
		public ShiftController(ShiftRepository<MsSqlDbContext> shiftRepository, TypeShiftRepository<MsSqlDbContext> typeShiftRepository, IMapper mapper, IEventLogService eventLogService) : base(mapper, eventLogService)
		{
			_shiftRepository = shiftRepository;
			_typeShiftRepository = typeShiftRepository;
		}

	
		/// <summary>
		/// Vrátí všechny typy směn.
		/// </summary>
		/// <returns>Seznam typů směn.</returns>
		[HttpGet("api/v1/shifts/types")]
		public async Task<ActionResult<List<TypeShiftBaseDto>>> GetShiftTypesAsync()
		{
			try
			{
				var shiftTypes = await _typeShiftRepository.GetAllAsync();
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
				_eventLogService.LogError(Guid.Parse("87dc3ad8-7ca7-426f-96cf-0e4d7bf9a8e4"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	

		/// <summary>
		/// Vrátí všechny směny.
		/// </summary>
		/// <returns>Seznam směn.</returns>
		[HttpGet("api/v1/shifts")]
		public async Task<ActionResult<List<ShiftBaseDto>>> GetShiftsAsync()
		{
			try
			{
				var shifts = await _shiftRepository.GetAllAsync();
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
				_eventLogService.LogError(Guid.Parse("50a7d33c-e03d-4362-bc4b-652979794a6b"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Uloží seznam směn do databáze. Aktualizuje existující směny, přidá nové a odstraní ty, které již nejsou v seznamu.
		/// Vždy je potřeba zadat celý měsíc, jinak budou ostatní dny smazány.
		/// </summary>
		/// <param name="shiftsDto">Seznam směn k uložení.</param>
		/// <returns>Seznam uložených směn.</returns>
		[HttpPut("api/v1/shifts")]
		public async Task<ActionResult<List<ShiftBaseDto>>> PutShiftsAsync([FromBody] List<ShiftBaseDto> shiftsDto)
		{
			try
			{
				if (shiftsDto != null)
				{
					var shifts = _mapper.Map<List<Shift>>(shiftsDto);
					var result = _shiftRepository.Save(shifts);
					return result ? Ok(shiftsDto) : Problem();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.LogError(Guid.Parse("9ab1191b-6ca7-48af-8d0d-083b9c8c9254"), ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}