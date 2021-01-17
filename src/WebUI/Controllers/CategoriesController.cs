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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult CategoryPage(int id)
        {
            //var category = _categoryService.Get
            return View();
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
