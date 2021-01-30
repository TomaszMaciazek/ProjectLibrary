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
    [Authorize]
    public class ProlongRequestsController : Controller
    {
        private readonly IProlongRequestService _prolongRequestService;
        public ProlongRequestsController(IProlongRequestService prolongRequestService)
        {
            _prolongRequestService = prolongRequestService;
        }

        [HttpGet]
        [Description("Get awaiting prolong requests")]
        public async Task<IActionResult> Index()
        {
            var requests = await _prolongRequestService.GetAllAwaitingProlongRequestsAsync();
            return View(requests);
        }

        [HttpPost]
        [Description("Add prolong request to database")]
        public async Task<IActionResult> CreateProlongRequest(AddProlongRequestVM prolongRequestVM)
        {
            try
            {
                await _prolongRequestService.AddProlongRequestAsync(prolongRequestVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update prolong request in database")]
        public async Task<IActionResult> UpdateProlongRequest([FromBody] UpdateProlongRequestVM prolongRequestVM)
        {
            try
            {
                await _prolongRequestService.UpdateProlongRequestAsync(prolongRequestVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete prolong request from database")]
        public async Task<IActionResult> DeleteProlongRequest(int id)
        {
            try
            {
                await _prolongRequestService.DeleteProlongRequestAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }
    }
}
