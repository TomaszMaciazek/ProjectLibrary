using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPublisherService
    {
        Task AddPublisherAsync(AddPublisherVM publisher);
        Task DeletePublisherAsync(int id);
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(UpdatePublisherVM publisher);
    }
}