using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _config = configuration;
        }

        [HttpGet("Admins")]
        [Description("Get all administrators")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAdmins()
        {
            var librarians = await _userService.GetAllAdminsAsync();
            return Ok(librarians);
        }

        [HttpGet("Librarians")]
        [Description("Get all librarians")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetLibrarians()
        {
            var librarians = await _userService.GetAllLibrariansAsync();
            return Ok(librarians);
        }

        [HttpGet("Readers")]
        [Description("Get all readers")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetReaders()
        {
            var readers = await _userService.GetAllReadersAsync();
            return Ok(readers);
        }

        [HttpGet("Users/{id}")]
        [Description("Get user by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
        {
            var currentUser = await _userService.GetUserByClaimsAsync(HttpContext.User);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                if (currentUser.RoleName == "Librarian")
                {
                    if(user.RoleName != "Reader" && user.Id != currentUser.Id)
                    {
                        return Forbid(); //Librarian cannot access users other 
                    }
                    
                }
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("Readers/{id}")]
        [Description("Get reader by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<UserDto>> GetReaderById([FromRoute] int id)
        {
            var reader = await _userService.GetUserByIdAsync(id);
            if (reader == null || await _userService.IsReader(reader.Id) == false)
            {
                return NotFound();
            }
            return Ok(reader);
        }

        [HttpGet("Librarians/{id}")]
        [Description("Get librarian by id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetLibrarianById([FromRoute]int id)
        {
            var librarian = await _userService.GetUserByIdAsync(id);
            if (librarian == null || await _userService.IsLibrarian(librarian.Id) == false)
            {
                return NotFound();
            }
            return Ok(librarian);
        }

        [HttpGet("Admins/{id}")]
        [Description("Get admin by id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetAdminById([FromRoute] int id)
        {
            var admin = await _userService.GetUserByIdAsync(id);
            if (admin == null || await _userService.IsAdmin(admin.Id) == false)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpGet("CurrentUser")]
        [Description("Get current user")]
        [Authorize(Roles = "Admin, Librarian, Reader")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userService.GetUserByClaimsAsync(HttpContext.User);
            return Ok(user);
        }

        [HttpPost("Admins")]
        [Description("Creates user with Admin role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAdmin([FromBody] CreateAdminOrLibrarianVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userVM);
            }
            var user = await _userService.CreateAdminAsync(userVM);
            if (user == null)
            {
                return BadRequest();
            }
            return Created($"api/users/admins/{user.Id}", null);
        }

        [HttpPost("Librarians")]
        [Description("Creates user with Librarian role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostLibrarian([FromBody] CreateAdminOrLibrarianVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userVM);
            }
            var user = await _userService.CreateLibrarianAsync(userVM);
            if (user == null)
            {
                return BadRequest();
            }
            return Created($"api/users/librarians/{user.Id}", null);
        }

        [HttpPost("Readers")]
        [Description("Creates user with Reader role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostReader([FromBody] CreateReaderVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userVM);
            }
            var user = await _userService.CreateReaderAsync(userVM);
            if(user == null)
            {
                return BadRequest();
            }
            return Created($"api/users/readers/{user.Id}",null);
        }

        
        [HttpPost]
        [Description("Sign In user")]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInUser([FromBody]UserVM userVM)
        {
            try
            {
                IActionResult response = Unauthorized();
                var user = await _userService.SignInUserAsync(userVM);
                if (user != null)
                {
                    var tokenString = JwtTokenGenerator.JwtTokenGenerator.GenerateJsonWebToken(user, _config);
                    return Ok(new { token = tokenString });
                }
                return response;
            }
            catch (NotFoundException)
            {
                return Unauthorized(new { message = "user not found" });
            }
            catch (UserIsLockedOutException)
            {
                return Unauthorized(new {message = "user is deactivated"});
            }
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

        [HttpPut("Deactivate/{id}")]
        [Description("Make user not active")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> DeactivateUser([FromRoute] int id)
        {
            var currentUser = await _userService.GetUserByClaimsAsync(HttpContext.User);
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                if(currentUser.RoleName == "Librarian")
                {
                    if(user.Id == currentUser.Id || user.RoleName != "Reader")
                    {
                        return Forbid(); //Librarian cannot deactivate himself or user with role other than Reader
                    }
                }
                var result = await _userService.MakeUserNotActive(id);
                if (result)
                {
                    return NoContent();
                }
            }
            return NotFound();
        }

        [HttpPut("Activate/{id}")]
        [Description("Make user active")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> ActivateUser([FromRoute] int id)
        {
            var currentUser = await _userService.GetUserByClaimsAsync(HttpContext.User);
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                if (currentUser.RoleName == "Librarian")
                {
                    if (user.Id == currentUser.Id || user.RoleName != "Reader")
                    {
                        return Forbid(); //Librarian cannot activate himself or user with role other than Reader
                    }
                }
                var result = await _userService.MakeUserActive(id);
                if (result)
                {
                    return NoContent();
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Description("Delete user")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                if (result != null)
                {
                    if(await _userService.IsLibrarian(id) || await _userService.IsAdmin(id))
                    {
                        if (HttpContext.User.IsInRole("Librarian"))
                        {
                            return Forbid(); //Librarian cannot delete librarians and admins
                        }
                    }
                    await _userService.DeleteUserAsync(id);
                    return NoContent();
                }
                return NotFound();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
            catch (DeleteIsForbiddenException)
            {
                return Forbid();
            }
        }
    }
}
