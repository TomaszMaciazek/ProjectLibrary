using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorAndBookRepository : BaseRepository<AuthorAndBook>, IAuthorAndBookRepository
    {
        protected override DbSet<AuthorAndBook> DbSet => _dbContext.AuthorAndBooks;
        public AuthorAndBookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<AuthorAndBook>> GetAllRelations()
        {
            return await DbSet
                .Include(ab => ab.Author)
                .Include(ab => ab.Book)
                .ToListAsync();
        }

    }
}
