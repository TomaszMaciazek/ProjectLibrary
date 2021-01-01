using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<ICollection<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
    }
}