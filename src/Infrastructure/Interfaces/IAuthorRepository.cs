using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<ICollection<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
    }
}