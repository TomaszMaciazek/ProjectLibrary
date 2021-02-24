using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<ICollection<Book>> GetAllBooksAsync(
            string filterTitleString,
            string[] authors,
            string[] categories,
            string[] publishers,
            bool onlyAvailable
            );
        Task<Book> GetBookByIdAsync(int id);
    }
}