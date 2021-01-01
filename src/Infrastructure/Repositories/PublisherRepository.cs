using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        protected override DbSet<Publisher> DbSet => _dbContext.Publishers;
        public PublisherRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<Publisher>> GetAllPublishers() => await DbSet.ToListAsync();
        public async Task<Publisher> GetPublisherById(int id)
        {
            return await DbSet.Include(p => p.Books).Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
