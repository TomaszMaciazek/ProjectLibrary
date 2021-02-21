using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public abstract class BaseRepository<Entity> where Entity : BaseEntity
    {
        protected LibraryDbContext _dbContext;
        protected abstract DbSet<Entity> DbSet { get; }
        public BaseRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Entity entity)
        {
            DbSet.Add(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Entity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Entity entity)
        {
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }

    }
}
