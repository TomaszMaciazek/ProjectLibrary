using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProlongRequestsController : ControllerBase
    {
        private readonly IProlongRequestService _prolongRequestService;
        public ProlongRequestsController(IProlongRequestService prolongRequestService)
        {
            _prolongRequestService = prolongRequestService;
        }


        [HttpGet]
        [Description("Get all prolong requests")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<ProlongRequestDto>>> Get()
        {
            var requests = await _prolongRequestService.GetAllProlongRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        [Description("Get prolong request by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<ProlongRequestDto>> Get([FromRoute] int id)
        {
            var request = await _prolongRequestService.GetProlongRequestByIdAsync(id);
            if(request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        [HttpPost]
        [Description("Add prolong request to database")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Post([FromBody]AddProlongRequestVM prolongRequesrVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(prolongRequesrVM);
            }
            try
            {
                var request = await _prolongRequestService.AddProlongRequestAsync(prolongRequesrVM);
                return Created($"api/prolongRequests/{request.Id}",null);
            }
            catch (AddOperationFailedException) {
                return Problem();
            }
        }

        [HttpPut]
        [Description("Update prolong request in database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Put([FromBody]UpdateProlongRequestVM prolongRequestVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(prolongRequestVM);
            }
            try
            {
                var result = await _prolongRequestService.UpdateProlongRequestAsync(prolongRequestVM);
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
        [Description("Delete prolong request from database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _prolongRequestService.DeleteProlongRequestAsync(id);
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
