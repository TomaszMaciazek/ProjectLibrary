using Domain.Common;
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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        protected override DbSet<Book> DbSet => _dbContext.Books;
        public BookRepository(LibraryDbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Book>> GetAllBooksAsync(string filterString)
        {
            if (string.IsNullOrEmpty(filterString))
            {
                return await DbSet
                    .Include(b => b.Authors)
                        .ThenInclude(ab => ab.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Publisher)
                    .ToListAsync();
            }
            return await DbSet
                    .Include(b => b.Authors)
                        .ThenInclude(ab => ab.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Publisher)
                    .Where(b => b.Title.Contains(filterString))
                    .ToListAsync();
        }
        public async Task<ICollection<Book>> GetAllAvailableBooksAsync(string filterString)
        {
            if (string.IsNullOrEmpty(filterString))
            {
                return await DbSet
                    .Include(b => b.Authors)
                        .ThenInclude(ab => ab.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Publisher)
                    .Where(b => (GetNumberOfAwaitingReservations(b) + GetNumberOfCurrentBorrowings(b)) < b.Count)
                    .ToListAsync();
            }
            return await DbSet
                    .Include(b => b.Authors)
                        .ThenInclude(ab => ab.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Publisher)
                    .Where(
                        b => b.Title.Contains(filterString)
                        && (GetNumberOfAwaitingReservations(b) + GetNumberOfCurrentBorrowings(b)) < b.Count
                     )
                    .ToListAsync();
        }
        
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await DbSet
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.Authors).ThenInclude(ab => ab.Author)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();
        }

        private int GetNumberOfAwaitingReservations(Book book)
            => book.Reservations.Where(r => r.ReservationStatus == StatusEnum.Awaiting).Count();

        private int GetNumberOfCurrentBorrowings(Book book)
            => book.Borrowings.Where(b => !b.ReturnedByUser.HasValue).Count();
    }
}
