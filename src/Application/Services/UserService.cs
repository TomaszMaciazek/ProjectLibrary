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
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateLibrarianAsync(UserVM newUser)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    UserName = PolishCharactersChanger.ChangePolishCharatersInString(newUser.Name.Replace(" ", "")).ToLower()
                };
                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result == IdentityResult.Success)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "Librarian");
                    if (resultRole != IdentityResult.Success)
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task CreateReaderAsync(UserVM newUser)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    UserName = PolishCharactersChanger.ChangePolishCharatersInString(newUser.Name.Replace(" ", "")).ToLower(),
                    CardNumber = newUser.CardNumber,
                    BorrowingsLimit = 5,
                    ReservationsLimit = 5

                };
                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result == IdentityResult.Success)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "Reader");
                    if (resultRole != IdentityResult.Success)
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task DeleteUserAsync(int id)
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
                }
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }

        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

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

        public async Task UpdateUserAsync(UpdateUserVM userToUpdate)
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
                }
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }

        }
    }
}
