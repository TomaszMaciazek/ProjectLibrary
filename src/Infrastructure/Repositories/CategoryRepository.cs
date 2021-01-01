using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        protected override DbSet<Category> DbSet => _dbContext.Categories;
        public CategoryRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Category>> GetAllCategories() => await DbSet.ToListAsync();
        public async Task<Category> GetCategoryById(int id) => await DbSet.FindAsync(id);
        public async Task<ICollection<Book>> GetAllBooksFromCategory(int categoryId)
        {
            return await DbSet
                .Include(c => c.Books)
                    .ThenInclude(b => b.Category)
                .Include(c => c.Books)
                    .ThenInclude(b => b.Publisher)
                .Include(c => c.Books)
                    .ThenInclude(b => b.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(c => c.Id == categoryId)
                .Select(c => c.Books)
                .SingleOrDefaultAsync();
        }
    }
}
