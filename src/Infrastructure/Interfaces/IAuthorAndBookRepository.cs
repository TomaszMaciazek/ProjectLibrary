using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAuthorAndBookRepository : IRepository<AuthorAndBook>
    {
        Task<ICollection<AuthorAndBook>> GetAllRelationsAsync();
        Task ChangeBookRelationsAsync(ICollection<int> authorsIds, int bookId);
    }
}