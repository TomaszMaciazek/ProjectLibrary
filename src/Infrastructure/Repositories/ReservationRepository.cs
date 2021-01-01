using Domain.Common;
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
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        protected override DbSet<Reservation> DbSet => _dbContext.Reservations;
        public ReservationRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Reservation>> GetAllReservations()
        {
            return await DbSet
                .Include(r => r.Book)
                    .ThenInclude(book => book.Category)
                .Include(r => r.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(r => r.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(r => r.ReservationStatus == ReservationStatusEnum.Awaiting)
                .ToListAsync();
        }
        public async Task<ICollection<Reservation>> GetAllAwaitingReservations()
        {
            return await DbSet
                .Include(r => r.Book)
                    .ThenInclude(book => book.Category)
                .Include(r => r.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(r => r.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(r => r.ReservationStatus == ReservationStatusEnum.Awaiting)
                .ToListAsync();

        }
        public async Task<Reservation> GetReservationById(int id)
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
        public async Task<ICollection<Reservation>> GetAllUserReservations(int userId)
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

        public async Task<ICollection<Reservation>> GetAllUserAwaitingReservations(int userId)
        {
            return await DbSet
                .Include(b => b.Book)
                    .ThenInclude(book => book.Category)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Publisher)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Authors)
                        .ThenInclude(ab => ab.Author)
                .Where(b => b.UserId == userId && b.ReservationStatus == ReservationStatusEnum.Awaiting)
                .ToListAsync();
        }
    }
}
