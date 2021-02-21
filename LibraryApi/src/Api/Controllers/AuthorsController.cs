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
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Description("Get all authors")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get(){
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [Description("Get author by id")]
        public async Task<ActionResult<AuthorDto>> Get([FromRoute] int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if(author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        [Description("Add author to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Post([FromBody]AddAuthorVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(authorVM);
            }
            try
            {
                var author = await _authorService.AddAuthorAsync(authorVM);
                return Created($"api/authors/{author.Id}",null);

            }
            catch (AddOperationFailedException) {
                return Problem();
            }
        }

        [HttpPut]
        [Description("Update author in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Put([FromBody]UpdateAuthorVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(authorVM);
            }
            try
            {
                var result = await _authorService.UpdateAuthorAsync(authorVM);
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
        [Description("Delete author from database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
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
