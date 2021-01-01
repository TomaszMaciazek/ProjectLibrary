using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAuthorAndBookRepository : IRepository<AuthorAndBook>
    {
        Task<ICollection<AuthorAndBook>> GetAllRelations();
    }
}