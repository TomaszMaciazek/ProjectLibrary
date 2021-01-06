using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        protected override DbSet<Category> DbSet => _dbContext.Categories;
        public CategoryRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Category>> GetAllCategoriesAsync() => await DbSet.ToListAsync();
        public async Task<Category> GetCategoryByIdAsync(int id) => await DbSet.FindAsync(id);
    }
}
