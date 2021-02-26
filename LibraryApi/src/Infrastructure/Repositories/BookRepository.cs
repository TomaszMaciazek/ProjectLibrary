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

        public async Task<ICollection<Book>> GetAllBooksAsync(
            string filterTitleString,
            string [] authors,
            string [] categories,
            string [] publishers,
            bool onlyAvailable,
            int pageNumber,
            int pageSize
            )
        {
            var set = DbSet
                    .Include(b => b.Authors)
                        .ThenInclude(ab => ab.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Publisher);

            var result = string.IsNullOrEmpty(filterTitleString)
                ? set : set.Where(b => b.Title.ToLower().Contains(filterTitleString.ToLower()));

            result = authors.Length == 0
                ? result
                : result.Where(b => b.Authors.Where(a => authors.Contains(a.Author.Name)).Count() > 0);

            result = categories.Length == 0
                ? result
                : result.Where(b => categories.Contains(b.Category.Name));

            result = publishers.Length == 0
                ? result
                : result.Where(b => publishers.Contains(b.Publisher.Name));

            result = onlyAvailable
                ? result
                    .Where(b => (GetNumberOfAwaitingReservations(b) + GetNumberOfCurrentBorrowings(b)) < b.Count)
                : result;
            return await result.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync() ;
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
