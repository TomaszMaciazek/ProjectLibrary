using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Description("Get all reservations")]
        [Authorize(Roles = "Admin, Librarian")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Description("Add reservation to database")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> CreateReservation([FromBody] AddReservationVM publisherVM)
        {
            try
            {
                await _reservationService.AddReservationAsync(publisherVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update reservation in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> UpdateReservation([FromBody] UpdateReservationVM publisherVM)
        {
            try
            {
                await _reservationService.UpdateReservationAsync(publisherVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete reservation from database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }
    }
}
