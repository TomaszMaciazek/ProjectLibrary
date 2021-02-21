using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> AddCategoryAsync(AddCategoryVM category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryWithBooksDto> GetCategoryByIdAsync(int id);
        Task<bool> UpdateCategoryAsync(UpdateCategoryVM category);
    }
}