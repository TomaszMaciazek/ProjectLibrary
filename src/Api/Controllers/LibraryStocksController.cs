using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Http;
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
    public class LibraryStocksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;

        public LibraryStocksController(
            IBookService bookService,
            ICategoryService categoryService,
            IPublisherService publisherService,
            IAuthorService authorService
            )
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _publisherService = publisherService;
            _authorService = authorService;
        }

        [HttpGet]
        [Description("Get all books and filter data")]
        public async Task<ActionResult<BooksPageVM>> Get()
        {
            var books = await _bookService.GetAllBooksAsync(null);
            var publishers = await _publisherService.GetAllPublishersAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            var authors = await _authorService.GetAllAuthorsAsync();
            var model = new BooksPageVM
            {
                Books = books,
                Publishers = publishers,
                Categories = categories,
                Authors = authors
            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        [Description("Get book by id")]
        public async Task<ActionResult<BookWithDetalisDto>> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        [Description("Adds book to database")]
        public async Task<IActionResult> Post([FromBody]AddBookVM bookVM)
        {
            try
            {
                await _bookService.AddBookAsync(bookVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }

        [HttpPut]
        [Description("Updates book in database")]
        public async Task<IActionResult> Put([FromBody]UpdateBookVM bookVM)
        {
            try
            {
                await _bookService.UpdateBookAsync(bookVM);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Removes book from database")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
            }
            catch (DeleteIsForbiddenException) { }
            catch (DeleteOperationFailedException) { }
            return Ok();
        }
    }
}
