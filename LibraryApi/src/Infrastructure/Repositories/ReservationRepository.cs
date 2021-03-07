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
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        protected override DbSet<Reservation> DbSet => _dbContext.Reservations;
        public ReservationRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Reservation>> GetAllReservationsAsync(int? pageNumber, int? pageSize, bool onlyAwaiting)
        {
            var reservations = DbSet
                .Include(r => r.Book)
                    .ThenInclude(book => book.Category)
                .Include(r => r.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(r => r.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author);

            var result = onlyAwaiting
                ? reservations.Where(r => r.ReservationStatus == StatusEnum.Awaiting)
                : reservations;

            return (pageNumber.HasValue && pageSize.HasValue)
                ? await result.Skip(pageNumber.Value * pageSize.Value).ToListAsync()
                : await result.ToListAsync();
        }
        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await DbSet
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
        public async Task<ICollection<Reservation>> GetAllUserReservationsAsync(int userId, int? pageNumber, int? pageSize, bool onlyAwaiting)
        {
            var reservations = DbSet
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.UserId == userId);

            var result = onlyAwaiting
                ? reservations.Where(r => r.ReservationStatus == StatusEnum.Awaiting)
                : reservations;

            return (pageNumber.HasValue && pageSize.HasValue)
                ? await result.Skip(pageNumber.Value * pageSize.Value).ToListAsync()
                : await result.ToListAsync();
        }
    }
}
