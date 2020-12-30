using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task Add(Entity entity)
        {
            DbSet.Add(entity);
            await SaveChangesAsync();
        }

        public async Task Update(Entity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }


    }
}
