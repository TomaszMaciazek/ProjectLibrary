using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<ICollection<Book>> GetAllBooksAsync(string filterString);
        Task<ICollection<Book>> GetAllAvailableBooksAsync(string filterString);
        Task<Book> GetBookByIdAsync(int id);
    }
}