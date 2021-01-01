using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<ICollection<Book>> GetAllBooks();
        Task<ICollection<Book>> GetAllAvailableBooks();
        Task<Book> GetBookById(int id);
    }
}