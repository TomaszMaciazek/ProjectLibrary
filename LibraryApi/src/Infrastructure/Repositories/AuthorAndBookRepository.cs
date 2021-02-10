using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ICollection<AuthorAndBook>> GetAllRelationsAsync()
        {
            return await DbSet
                .Include(ab => ab.Author)
                .Include(ab => ab.Book)
                .ToListAsync();
        }

        public async Task ChangeBookRelationsAsync(ICollection<int> authorsIds, int bookId)
        {
            var relations = DbSet.Where(ab => ab.BookId == bookId);
            ICollection<int> idsOfAuthorsToDelete = new List<int>();
            foreach(var relation in relations)
            {
                if (!authorsIds.Contains(relation.AuthorId))
                {
                    idsOfAuthorsToDelete.Add(relation.Id);
                }
            }
            var relationsToDelete = DbSet.Where(r => idsOfAuthorsToDelete.Contains(r.Id));
            DbSet.RemoveRange(relationsToDelete);
            var idsOfAuthorsToAdd = authorsIds.Where(x => !relations.Select(y => y.AuthorId).Contains(x));
            foreach(var id in idsOfAuthorsToAdd)
            {
                AuthorAndBook newRelation = new AuthorAndBook
                {
                    AuthorId = id,
                    BookId = bookId
                };
                DbSet.Add(newRelation);
            }
            await SaveChangesAsync();
        }
    }
}
