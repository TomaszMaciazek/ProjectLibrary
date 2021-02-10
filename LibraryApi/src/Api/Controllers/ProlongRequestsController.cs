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
        public async Task<ActionResult<ICollection<ProlongRequestDto>>> Get()
        {
            var requests = await _prolongRequestService.GetAllProlongRequestsAsync();
            return Ok(requests);
        }

        [HttpPost]
        [Description("Add prolong request to database")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Post([FromBody]AddProlongRequestVM prolongRequesrVM)
        {
            try
            {
                await _prolongRequestService.AddProlongRequestAsync(prolongRequesrVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }

        [HttpPut]
        [Description("Update prolong request in database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Put([FromBody]UpdateProlongRequestVM prolongRequestVM)
        {
            try
            {
                await _prolongRequestService.UpdateProlongRequestAsync(prolongRequestVM);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete prolong request from database")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prolongRequestService.DeleteProlongRequestAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            return Ok();
        }
    }
}
