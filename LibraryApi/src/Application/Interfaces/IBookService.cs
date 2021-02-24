using Application.Args;
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
        Task<bool> DeleteBookAsync(int id);
        Task<ICollection<BaseBookDto>> GetAllBooksAsync(BookFilterArgs args);
        Task<BookWithDetalisDto> GetBookByIdAsync(int id);
        Task<bool> UpdateBookAsync(UpdateBookVM book);
    }
}