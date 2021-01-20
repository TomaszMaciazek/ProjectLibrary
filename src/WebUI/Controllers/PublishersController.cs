using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [Description("Show all publishers")]
        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return View(publishers);
        }

        [HttpGet("{id}")]
        [Description("Show details of publisher with given id")]
        public async Task<IActionResult> PublisherPage(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return View(publisher);
        }

        [HttpPost]
        [Description("Add publisher to database")]
        public async Task<IActionResult> CreatePublisher([FromBody] AddPublisherVM publisherVM)
        {
            try
            {
                await _publisherService.AddPublisherAsync(publisherVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update publisher in database")]
        public async Task<IActionResult> UpdatePublisher([FromBody] UpdatePublisherVM publisherVM)
        {
            try
            {
                await _publisherService.UpdatePublisherAsync(publisherVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete publisher from database")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }
    }
}
