using Application.Dto;
using Application.ViewModels;
using Domain.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task CreateLibrarianAsync(UserVM newUser);
        Task CreateReaderAsync(UserVM newUser);
        Task DeleteUserAsync(int id);
        Task<ICollection<UserDto>> GetAllLibrariansAsync();
        Task<ICollection<UserDto>> GetAllReadersAsync();
        Task<ApplicationUser> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UpdateUserVM userToUpdate);
    }
}