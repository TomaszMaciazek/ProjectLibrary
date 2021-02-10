using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(AddAuthorVM author);
        Task DeleteAuthorAsync(int id);
        Task<ICollection<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorWithBooksDto> GetAuthorByIdAsync(int id);
        Task UpdateAuthorAsync(UpdateAuthorVM author);
    }
}