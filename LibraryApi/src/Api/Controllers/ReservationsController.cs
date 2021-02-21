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
        public async Task<ActionResult<IEnumerable<ReservationDto>>> Get()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
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

        [HttpGet("awaitingReservations")]
        [Description("Get all awaiting reservations")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAwaitingReservations()
        {
            var reservations = await _reservationService.GetAllAwaitingReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet]
        [Description("Get all user reservations")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetUserReservations([FromQuery] int userId)
        {
            var reservations = await _reservationService.GetAllUserReservationsAsync(userId);
            return Ok(reservations);
        }

        [HttpGet("awaitingReservations")]
        [Description("Get all user reservations")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetUserAwaitingReservations([FromQuery] int userId)
        {
            var reservations = await _reservationService.GetAllUserAwaitingReservationsAsync(userId);
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
                await _reservationService.UpdateReservationAsync(reservationVm);
                return Ok();
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
                await _reservationService.DeleteReservationAsync(id);
                return Ok();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
        }
    }
}
