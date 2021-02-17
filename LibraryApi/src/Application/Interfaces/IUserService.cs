using Application.Dto;
using Application.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task CreateLibrarianAsync(CreateLibrarianVM newUser);
        Task CreateReaderAsync(CreateReaderVM newUser);
        Task DeleteUserAsync(int id);
        Task<ICollection<UserDto>> GetAllLibrariansAsync();
        Task<ICollection<UserDto>> GetAllReadersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetCurrentUser(ClaimsPrincipal claims);
        Task<bool> IsLibrarian(int id);
        Task<bool> IsReader(int id);
        Task MakeUserActive(int id);
        Task MakeUserNotActive(int id);
        Task<UserWithRoleDto> SignInUserAsync(UserVM userVM);
        Task SignOutUserAsync();
        Task UpdateUserAsync(UpdateUserVM userToUpdate);
    }
}