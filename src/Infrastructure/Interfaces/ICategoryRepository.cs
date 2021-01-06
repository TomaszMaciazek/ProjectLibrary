using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<ICollection<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
    }
}