using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Task<Book> GetBookById(int id);
    }
}