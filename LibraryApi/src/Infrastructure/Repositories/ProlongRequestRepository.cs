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
    public class ProlongRequestRepository : BaseRepository<ProlongRequest>, IProlongRequestRepository
    {
        protected override DbSet<ProlongRequest> DbSet => _dbContext.ProlongRequests;
        public ProlongRequestRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<ProlongRequest>> GetAllProlongRequestsAsync()
            => await DbSet
            .Include(r => r.User)
            .Include(r => r.Borrowing)
                .ThenInclude(b => b.Book)
            .ToListAsync();

        public async Task<ICollection<ProlongRequest>> GetAllAwaitingProlongRequestsAsync()
            => await DbSet
            .Include(r => r.User)
            .Include(r => r.Borrowing)
                .ThenInclude(b => b.Book)
            .Where(r => r.Status == StatusEnum.Awaiting)
            .ToListAsync();

        public async Task<ProlongRequest> GetProlongRequestByIdAsync(int id)
            => await DbSet
            .Include(r => r.User)
            .Include(r => r.Borrowing)
                .ThenInclude(b => b.Book)
            .Where(r => r.Id == id)
            .SingleOrDefaultAsync();

        public async Task<ICollection<ProlongRequest>> GetAllUserProlongRequestsAsync(int userId)
            => await DbSet
            .Include(r => r.User)
            .Include(r => r.Borrowing)
                .ThenInclude(b => b.Book)
            .Where(r => r.UserId == userId)
            .ToListAsync();
        public async Task<ICollection<ProlongRequest>> GetAllUserAwaitingProlongRequestsAsync(int userId)
            => await DbSet
            .Include(r => r.User)
            .Include(r => r.Borrowing)
                .ThenInclude(b => b.Book)
            .Where(r => r.UserId == userId && r.Status == StatusEnum.Awaiting)
            .ToListAsync();
    }
}
