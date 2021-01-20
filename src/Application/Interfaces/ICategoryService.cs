using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(AddCategoryVM category);
        Task DeleteCategoryAsync(int id);
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryWithBooksDto> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(UpdateCategoryVM category);
    }
}