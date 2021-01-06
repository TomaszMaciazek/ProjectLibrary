using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(AddOrUpdateCategoryDto category);
        Task DeleteCategoryAsync(int id);
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync(int id);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(AddOrUpdateCategoryDto category);
    }
}