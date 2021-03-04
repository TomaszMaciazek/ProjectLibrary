using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Common;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInController;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInController,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInController = signInController;
            _mapper = mapper;
        }

        public async Task<UserWithRoleDto> SignInUserAsync(UserVM userVM)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userVM.Username);
                user ??= await _userManager.FindByNameAsync(userVM.Username);
                if(user != null)
                {
                    var result = await _signInController.PasswordSignInAsync(user, userVM.Password, userVM.RememberMe, false);
                    if(result == SignInResult.Success)
                    {
                        var userDto = _mapper.Map<UserWithRoleDto>(user);
                        var roles = await _userManager.GetRolesAsync(user);
                        userDto.RoleName = roles[0];
                        return userDto;
                    }
                }
                throw new NotFoundException();
            }
            catch (UserIsLockedOutException ex)
            {
                throw ex;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception) {
                throw new SignInOperationFailedException();
            }
        }

        public async Task SignOutUserAsync()
        {
                await _signInController.SignOutAsync();
        }

        public async Task<UserDto> CreateLibrarianAsync(CreateLibrarianVM newUser)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    UserName = PolishCharactersChanger.ChangePolishCharatersInString(newUser.Name.Replace(" ", "")).ToLower(),
                    IsActive = true
                };
                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result == IdentityResult.Success)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "Librarian");
                    if (resultRole != IdentityResult.Success)
                    {
                        await _userManager.DeleteAsync(user);
                        return null;
                    }
                    return _mapper.Map<UserDto>(user);
                }
                return null;
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task<UserDto> CreateReaderAsync(CreateReaderVM newUser)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    UserName = PolishCharactersChanger.ChangePolishCharatersInString(newUser.Name.Replace(" ", "")).ToLower(),
                    CardNumber = newUser.CardNumber,
                    BorrowingsLimit = newUser.BorrowingsLimit ?? 5,
                    ReservationsLimit = newUser.ReservationsLimit ?? 5,
                    IsActive = true
                };
                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result == IdentityResult.Success)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "Reader");
                    if (resultRole != IdentityResult.Success)
                    {
                        await _userManager.DeleteAsync(user);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                if (id == 1)
                {
                    //prevents from deleting admin user
                    throw new DeleteIsForbiddenException();
                }
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }

        public async Task<bool> MakeUserActive(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if(user == null)
                {
                    return false;
                }
                else if (!user.IsActive)
                {
                    user.IsActive = true;
                    await _userManager.UpdateAsync(user);
                }
                return true;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }

        public async Task<bool> MakeUserNotActive(int id)
        {
            try
            {
                if (id == 1)
                {
                    //prevents from making admin user not active
                    throw new UpdateOperationFailedException();
                }
                var user = await _userManager.FindByIdAsync(id.ToString());
                if(user == null)
                {
                    return false;
                }
                else if (user.IsActive)
                {
                    user.IsActive = false;
                    await _userManager.UpdateAsync(user);
                }
                return true;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
            => _mapper.Map<UserDto>(await _userManager.FindByIdAsync(id.ToString()));

        public async Task<ICollection<UserDto>> GetAllLibrariansAsync()
        {
            List<UserDto> librarians = new List<UserDto>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Librarian"))
                {
                    librarians.Add(_mapper.Map<UserDto>(user));
                }
            }
            return librarians;
        }

        public async Task<ICollection<UserDto>> GetAllReadersAsync()
        {
            List<UserDto> librarians = new List<UserDto>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Reader"))
                {
                    librarians.Add(_mapper.Map<UserDto>(user));
                }
            }
            return librarians;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserVM userToUpdate)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userToUpdate.Id.ToString());
                if (user != null)
                {
                    PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
                    user.Email = string.IsNullOrEmpty(userToUpdate.Email) ? user.Email : userToUpdate.Email;
                    user.PasswordHash = string.IsNullOrEmpty(userToUpdate.Password)
                        ? user.PasswordHash : hasher.HashPassword(user, userToUpdate.Password);
                    user.ReservationsLimit = userToUpdate.ReservationsLimit ?? user.ReservationsLimit;
                    user.BorrowingsLimit = userToUpdate.BorrowingsLimit ?? user.BorrowingsLimit;
                    await _userManager.UpdateAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }

        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claims)
            => _mapper.Map<UserDto>(await _userManager.GetUserAsync(claims));

        public async Task<bool> IsLibrarian(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await _userManager.IsInRoleAsync(user, "Librarian");
        }

        public async Task<bool> IsReader(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await _userManager.IsInRoleAsync(user, "Reader");
        }
    }
}
