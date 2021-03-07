using Application.Args;
using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        
        [HttpGet]
        [Description("Get all reservations")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> Get(
            [FromQuery] bool onlyAwaiting = false,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
            )
        {

            var args = new ReservationsPaginationArgs
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyAwaiting = onlyAwaiting
            };

            var reservations = await _reservationService.GetAllReservationsAsync(args);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        [Description("Get reservation by id")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<ReservationDto>> Get([FromRoute] int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }
        
        [HttpGet("user/{id}")]
        [Description("Get all user reservations")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetUserReservations(
            [FromRoute] int id,
            [FromQuery] bool onlyAwaiting = false,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
            )
        {
            var args = new ReservationsPaginationArgs
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyAwaiting = onlyAwaiting
            };
            var reservations = await _reservationService.GetAllUserReservationsAsync(id, args);
            return Ok(reservations);
        }
        
        [HttpPost]
        [Description("Add reservation to database")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Post([FromBody]AddReservationVM reservationVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(reservationVm);
            }
            try
            {
                var reservation = await _reservationService.AddReservationAsync(reservationVm);
                return Created($"api/reservations/{reservation.Id}",null);
            }
            catch (AddOperationFailedException) {
                return Problem();
            }
        }

        [HttpPut]
        [Description("Update reservation in database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Put([FromBody]UpdateReservationVM reservationVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(reservationVm);
            }
            try
            {
                var result = await _reservationService.UpdateReservationAsync(reservationVm);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (UpdateOperationFailedException) {
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [Description("Delete reservation from database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _reservationService.DeleteReservationAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
        }
        
    }
}
