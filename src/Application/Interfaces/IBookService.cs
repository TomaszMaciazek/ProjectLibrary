using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(AddOrUpdateBookDto newBook);
        Task DeleteBookAsync(int id);
        Task<ICollection<BaseBookDto>> GetAllAvailableBooksAsync(string filterString);
        Task<ICollection<BaseBookDto>> GetAllBooksAsync(string filterString);
        Task<BookWithDetalisDto> GetBookByIdAsync(int id);
        Task UpdateBookAsync(AddOrUpdateBookDto book);
    }
}