using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        [Description("Show details of book with given id")]
        public async Task<IActionResult> BookPage(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return View(book);
        }

        [HttpPost]
        [Description("Add book to database")]
        public async Task<IActionResult> CreateBook([FromBody] AddBookVM bookVM)
        {
            try
            {
                await _bookService.AddBookAsync(bookVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update book in database")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookVM bookVM)
        {
            try
            {
                await _bookService.UpdateBookAsync(bookVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete book from database")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }

    }
}
