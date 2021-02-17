﻿using Application.Dto;
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
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [Description("Get all publishers")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<PublisherDto>>> Get()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        [Description("Get publisher by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<PublisherDto>> Get(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return Ok(publisher);
        }

        [HttpPost]
        [Description("Add publisher to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Post([FromBody] AddPublisherVM publisherVM)
        {
            try
            {
                await _publisherService.AddPublisherAsync(publisherVM);
                return Ok();
            }
            catch (AddOperationFailedException) {
                return Problem();
            }
        }

        [HttpPut]
        [Description("Update publisher in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Put([FromBody] UpdatePublisherVM publisherVM)
        {
            try
            {
                await _publisherService.UpdatePublisherAsync(publisherVM);
                return Ok();
            }
            catch (UpdateOperationFailedException) {
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [Description("Delete publisher from database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
                return Ok();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
            catch (DeleteIsForbiddenException) {
                return Forbid();
            }
        }
    }
}