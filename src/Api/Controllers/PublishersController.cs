using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [Description("Get all publishers")]
        public async Task<ActionResult<ICollection<PublisherDto>>> Get()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        [Description("Get publisher by id")]
        public async Task<ActionResult<PublisherDto>> Get(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return Ok(publisher);
        }

        [HttpPost]
        [Description("Add publisher to database")]
        public async Task<IActionResult> Post([FromBody] AddPublisherVM publisherVM)
        {
            try
            {
                await _publisherService.AddPublisherAsync(publisherVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }

        [HttpPut]
        [Description("Update publisher in database")]
        public async Task<IActionResult> Put([FromBody] UpdatePublisherVM publisherVM)
        {
            try
            {
                await _publisherService.UpdatePublisherAsync(publisherVM);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete publisher from database")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return Ok();
        }
    }
}
