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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Description("Get all categories")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<CategoryDto>>> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Description("Get category by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        [Description("Add category to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Post([FromBody]AddCategoryVM categoryVM)
        {
            try
            {
                await _categoryService.AddCategoryAsync(categoryVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }

        [HttpPut]
        [Description("Update category in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Put([FromBody]UpdateCategoryVM categoryVM)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(categoryVM);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete category from database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return Ok();
        }
    }
}
