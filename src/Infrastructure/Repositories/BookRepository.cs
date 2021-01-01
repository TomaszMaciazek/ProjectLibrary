using Domain.Common;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        protected override DbSet<Book> DbSet => _dbContext.Books;
        public BookRepository(LibraryDbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Book>> GetAllBooks() => await DbSet.ToListAsync();
        public async Task<ICollection<Book>> GetAllAvailableBooks() => await DbSet.Where(b => b.BookStatus == BookStatus.Available).ToListAsync();
        public async Task<Book> GetBookById(int id)
        {
            return await DbSet
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.Authors).ThenInclude(ab => ab.Author)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
