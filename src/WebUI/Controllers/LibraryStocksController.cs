using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class LibraryStocksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;

        [HttpGet]
        public async Task<IActionResult> Index(BooksSearchModel filterModel)
        {
            var books = filterModel.OnlyAwailable
                ? await _bookService.GetAllAvailableBooksAsync(filterModel.TitleFilterString) 
                : await _bookService.GetAllBooksAsync(filterModel.TitleFilterString);
            var categories = await _categoryService.GetAllCategoriesAsync();
            var publishers = await _publisherService.GetAllPublishersAsync();
            var authors = await _authorService.GetAllAuthorsAsync();
            var model = new BooksPageModel
            {
                Books = filterModel.OrderBy switch { 
                    "titleDesc" => books.OrderByDescending(b => b.Title),
                    _ => books.OrderBy(b => b.Title)
                },
                Authors = authors.OrderBy(a => a.Name),
                Publishers = publishers.OrderBy(p => p.Name),
                Categories = categories.OrderBy(c => c.Name)
            };
            return View(model);
        }
    }
}
