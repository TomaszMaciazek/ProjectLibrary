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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Description("Get all authors")]
        public async Task<ActionResult<ICollection<AuthorDto>>> Get(){
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [Description("Get author by id")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return Ok(author);
        }

        [HttpPost]
        [Description("Add author to database")]
        public async Task<IActionResult> Post([FromBody]AddAuthorVM authorVM)
        {
            try
            {
                await _authorService.AddAuthorAsync(authorVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }

        [HttpPut]
        [Description("Update author in database")]
        public async Task<IActionResult> Put([FromBody]UpdateAuthorVM authorVM)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(authorVM);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete author from database")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return Ok();
        }
    }
}
