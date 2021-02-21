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

        [HttpGet("readers/{id}")]
        [Description("Get reader by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<UserDto>> GetReaderById([FromRoute] int id)
        {
            var reader = await _userService.GetUserByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return Ok(reader);
        }

        [HttpGet("librarians/{id}")]
        [Description("Get librarian by id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetLibrarianById([FromRoute]int id)
        {
            var librarian = await _userService.GetUserByIdAsync(id);
            if (librarian == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> PostLibrarian([FromBody] CreateLibrarianVM userVM)
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

        [HttpPost]
        [Description("Creates user with Reader role")]
        [Route("[action]")]
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
        [Route("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateLibrarian([FromRoute] int id)
        {
            if(await _userService.IsLibrarian(id))
            {
                var result = await _userService.MakeUserNotActive(id);
                if (result)
                {
                    return NoContent();
                }
            }
            return NotFound();
        }

        [HttpPut]
        [Description("Make librarian active")]
        [Route("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateLibrarian([FromRoute] int id)
        {
            if (await _userService.IsLibrarian(id))
            {
                var result = await _userService.MakeUserActive(id);
                if (result)
                {
                    return NoContent();
                }
            }
            return NotFound();
        }

        [HttpPut]
        [Description("Make reader not active")]
        [Route("[action]/{id}")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> DeactivateReader([FromRoute] int id)
        {
            if (await _userService.IsReader(id))
            {
                var result = await _userService.MakeUserNotActive(id);
                if (result)
                {
                    return NoContent();
                }
            }
            return NotFound();
        }

        [HttpPut]
        [Description("Make reader active")]
        [Route("[action]/{id}")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> ActivateReader([FromRoute] int id)
        {
            if (await _userService.IsReader(id))
            {
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
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
