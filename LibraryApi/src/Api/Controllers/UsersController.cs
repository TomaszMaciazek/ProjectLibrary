using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Librarians")]
        [Description("Get all librarians")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLibrarians()
        {
            var librarians = await _userService.GetAllLibrariansAsync();
            return Ok(librarians);
        }

        [HttpGet("Readers")]
        [Description("Get all readers")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> GetReaders()
        {
            var readers = await _userService.GetAllReadersAsync();
            return Ok(readers);
        }

        [HttpGet("Readers/{id}")]
        [Description("Get reader by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<UserDto>> GetReaderById(int id)
        {
            var reader = await _userService.GetUserByIdAsync(id);
            return Ok(reader);
        }

        [HttpGet("Librarians/{id}")]
        [Description("Get librarian by id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetLibrarianById(int id)
        {
            var librarian = await _userService.GetUserByIdAsync(id);
            return Ok(librarian);
        }

        [HttpGet("CurrentUser")]
        [Description("Get current user")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            return Ok(user);
        }

        [HttpPost]
        [Description("Creates user with Librarian role")]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostLibrarian(CreateLibrarianVM userVM)
        {
            await _userService.CreateLibrarianAsync(userVM);
            return Ok();
        }

        [HttpPost]
        [Description("Creates user with Reader role")]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostReader(CreateReaderVM userVM)
        {
            await _userService.CreateReaderAsync(userVM);
            return Ok();
        }

        [HttpPost]
        [Description("Sign In user")]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<UserWithRoleDto>> SignInUser([FromBody]UserVM userVM)
        {
            var result = await _userService.SignInUserAsync(userVM);
            return Ok(result);
        }

        [HttpPost]
        [Description("Sign Out user")]
        [Route("[action]")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<UserWithRoleDto>> SignOutUser()
        {
            await _userService.SignOutUserAsync();
            return Ok();
        }

        [HttpPut]
        [Description("Make librarian not active")]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateLibrarian(int id)
        {
            if(await _userService.IsLibrarian(id))
            {
                await _userService.MakeUserNotActive(id);
            }
            return Ok();
        }

        [HttpPut]
        [Description("Make librarian active")]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateLibrarian(int id)
        {
            if (await _userService.IsLibrarian(id))
            {
                await _userService.MakeUserActive(id);
            }
            return Ok();
        }

        [HttpPut]
        [Description("Make reader not active")]
        [Route("[action]")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> DeactivateReader(int id)
        {
            if (await _userService.IsReader(id))
            {
                await _userService.MakeUserNotActive(id);
            }
            return Ok();
        }

        [HttpPut]
        [Description("Make reader active")]
        [Route("[action]")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> ActivateReader(int id)
        {
            if (await _userService.IsReader(id))
            {
                await _userService.MakeUserActive(id);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException)
            {
                return Forbid();
            }
            return Ok();
        }
    }
}
