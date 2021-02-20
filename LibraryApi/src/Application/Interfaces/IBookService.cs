using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<BookWithDetalisDto> AddBookAsync(AddBookVM newBook);
        Task DeleteBookAsync(int id);
        Task<ICollection<BaseBookDto>> GetAllAvailableBooksAsync(string filterString);
        Task<ICollection<BaseBookDto>> GetAllBooksAsync(string filterString);
        Task<BookWithDetalisDto> GetBookByIdAsync(int id);
        Task UpdateBookAsync(UpdateBookVM book);
    }
}