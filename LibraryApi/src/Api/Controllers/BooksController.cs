using Application.Args;
using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Description("Get all books and filter data")]
        [AllowAnonymous]
        public async Task<ActionResult<BooksPageVM>> Get(
            [FromQuery] string title,
            [FromQuery(Name = "author")] string [] authors,
            [FromQuery(Name = "category")] string [] categories,
            [FromQuery(Name = "publisher")] string[] publishers,
            [FromQuery] bool onlyAvailable,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
            )
        {
            var args = new BookFilterArgs
            {
                FilterTitleString = title,
                Authors = authors,
                Categories = categories,
                Publishers = publishers,
                OnlyAvailable = onlyAvailable,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var books = await _bookService.GetAllBooksAsync(args);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Description("Get book by id")]
        [AllowAnonymous]
        public async Task<ActionResult<BookWithDetalisDto>> Get([FromRoute] int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Description("Add book to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Post([FromBody]AddBookVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(bookVM);
            }
            try
            {
                var book = await _bookService.AddBookAsync(bookVM);
                return Created($"api/libraryStocks/{book.Id}",null);
            }
            catch (AddOperationFailedException) {
                return Problem();
            }
        }

        [HttpPut]
        [Description("Update book in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Put([FromBody]UpdateBookVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(bookVM);
            }
            try
            {
                var result = await _bookService.UpdateBookAsync(bookVM);
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
        [Description("Delete book from database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (DeleteIsForbiddenException) {
                return Forbid();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
        }
    }
}
