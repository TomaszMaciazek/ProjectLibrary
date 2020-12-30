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
    public class BookRepository : BaseRepository<Book>
    {
        protected override DbSet<Book> DbSet => _dbContext.Books;
        public BookRepository(LibraryDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Book> GetAllBooks()
        {
            return DbSet;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
