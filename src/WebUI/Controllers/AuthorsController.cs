using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Description("Show all authors")]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors);
        }

        [HttpGet("{id}")]
        [Description("Show details of author with given id")]
        public async Task<IActionResult> AuthorPage(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return View(author);
        }

        [HttpPost]
        [Description("Add author to database")]
        public async Task<IActionResult> CreateAuthor([FromBody] AddAuthorVM authorVM)
        {
            try
            {
                await _authorService.AddAuthorAsync(authorVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update author in database")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorVM authorVM)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(authorVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete author from database")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }
    }
}
