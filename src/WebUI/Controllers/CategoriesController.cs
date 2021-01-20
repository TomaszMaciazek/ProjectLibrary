using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Description("Show all categories")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet("{id}")]
        [Description("Show details of category with given id")]
        public async Task<IActionResult> CategoryPage(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [Description("Add category to database")]
        public async Task<IActionResult> CreateCategory([FromBody] AddCategoryVM categoryVM)
        {
            try
            {
                await _categoryService.AddCategoryAsync(categoryVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update category in database")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryVM categoryVM)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(categoryVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete category from database")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }
    }
}
