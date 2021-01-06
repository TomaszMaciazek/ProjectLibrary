using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BorrowingRepository : BaseRepository<Borrowing>, IBorrowingRepository
    {
        protected override DbSet<Borrowing> DbSet => _dbContext.Borrowings;
        public BorrowingRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Borrowing>> GetAllBorrowingsAsync() { 
            return await DbSet
                .Include(b => b.User)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .ToListAsync(); 
        }
        public async Task<ICollection<Borrowing>> GetAllNotReturnedBorrowingsAsync()
        {
            return await DbSet
                .Include(b => b.User)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => !b.ReturnedByUser.HasValue)
                .ToListAsync();
        }
        public async Task<Borrowing> GetBorrowingByIdAsync(int id)
        {
            return await DbSet
                .Include(b => b.User)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();
        }
        public async Task<ICollection<Borrowing>> GetAllUserBorrowingsAsync(int userId)
        {
            return await DbSet
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<ICollection<Borrowing>> GetAllUserNotReturnedBorrowingsAsync(int userId)
        {
            return await DbSet
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.UserId == userId && !b.ReturnedByUser.HasValue)
                .ToListAsync();
        }
    }
}
