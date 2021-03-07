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
        public async Task<ICollection<Borrowing>> GetAllBorrowingsAsync(int? pageNumber, int? pageSize, bool onlyNotReturned)
        {
            var borrowings = DbSet
               .Include(b => b.User)
               .Include(b => b.Book)
                   .ThenInclude(book => book.Category)
               .Include(b => b.Book)
                   .ThenInclude(book => book.Publisher)
               .Include(b => b.Book)
                   .ThenInclude(book => book.Authors)
                       .ThenInclude(ab => ab.Author);

            var result = onlyNotReturned
                    ? borrowings.Where(b => !b.ReturnedByUser.HasValue)
                    : borrowings;
            return (pageNumber.HasValue && pageSize.HasValue)
                ? await result.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value).ToListAsync()
                : await result.ToListAsync();
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
        public async Task<ICollection<Borrowing>> GetAllUserBorrowingsAsync(int userId, int? pageNumber, int? pageSize, bool onlyNotReturned)
        {
            var borrowings = DbSet
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.UserId == userId);

            var result = onlyNotReturned
                ? borrowings.Where(b => !b.ReturnedByUser.HasValue)
                : borrowings;

            return (pageNumber.HasValue && pageSize.HasValue)
                ? await result.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value).ToListAsync()
                : await result.ToListAsync();
        }
    }
}
