using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BorrowingRepository : BaseRepository<Borrowing>, IBorrowingRepository
    {
        protected override DbSet<Borrowing> DbSet => _dbContext.Borrowings;
        public BorrowingRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Borrowing>> GetAllBorrowings() { 
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
        public async Task<ICollection<Borrowing>> GetAllNotReturnedBorrowings()
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
                .Where(b => !b.ReturnDate.HasValue)
                .ToListAsync();
        }
        public async Task<Borrowing> GetBorrowingById(int id)
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
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<Borrowing>> GetAllUserBorrowings(int userId)
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

        public async Task<ICollection<Borrowing>> GetAllUserNotReturnedBorrowings(int userId)
        {
            return await DbSet
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.UserId == userId && !b.ReturnDate.HasValue)
                .ToListAsync();
        }
    }
}
