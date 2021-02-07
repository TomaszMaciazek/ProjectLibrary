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
        public async Task<ActionResult<ICollection<ReservationDto>>> Get()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("awaitingReservations")]
        [Description("Get all awaiting reservations")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<ICollection<ReservationDto>>> GetAwaitingReservations()
        {
            var reservations = await _reservationService.GetAllAwaitingReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        [Description("Get all user reservations")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<ICollection<ReservationDto>>> GetUserReservations(int id)
        {
            var reservations = await _reservationService.GetAllUserReservationsAsync(id);
            return Ok(reservations);
        }

        [HttpGet("awaitingReservations/{id}")]
        [Description("Get all user reservations")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<ICollection<ReservationDto>>> GetUserAwaitingReservations(int id)
        {
            var reservations = await _reservationService.GetAllUserAwaitingReservationsAsync(id);
            return Ok(reservations);
        }

        [HttpPost]
        [Description("Add reservation to database")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Post([FromBody]AddReservationVM reservationVm)
        {
            try
            {
                await _reservationService.AddReservationAsync(reservationVm);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }

        [HttpPut]
        [Description("Update reservation in database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Put([FromBody]UpdateReservationVM reservationVm)
        {
            try
            {
                await _reservationService.UpdateReservationAsync(reservationVm);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete reservation from database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            return Ok();
        }
    }
}
