using Application.Dto;
using Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateLibrarianAsync(CreateLibrarianVM newUser);
        Task<UserDto> CreateReaderAsync(CreateReaderVM newUser);
        Task<bool> DeleteUserAsync(int id);
        Task<ICollection<UserDto>> GetAllLibrariansAsync();
        Task<ICollection<UserDto>> GetAllReadersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetCurrentUser(ClaimsPrincipal claims);
        Task<bool> IsLibrarian(int id);
        Task<bool> IsReader(int id);
        Task<bool> MakeUserActive(int id);
        Task<bool> MakeUserNotActive(int id);
        Task<UserWithRoleDto> SignInUserAsync(UserVM userVM);
        Task SignOutUserAsync();
        Task<bool> UpdateUserAsync(UpdateUserVM userToUpdate);
    }
}