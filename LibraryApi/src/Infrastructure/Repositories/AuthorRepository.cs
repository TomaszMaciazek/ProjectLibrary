﻿using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        protected override DbSet<Author> DbSet => _dbContext.Authors;
        public AuthorRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Author>> GetAllAuthorsAsync() => await DbSet.ToListAsync();

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await DbSet.Include(a => a.Books)
            .ThenInclude(ab => ab.Book)
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync();
        }
    }
}
