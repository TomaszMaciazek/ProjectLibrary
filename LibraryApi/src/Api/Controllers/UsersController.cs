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
        private readonly ICurrentUserService _currerntUserService;

        public UsersController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currerntUserService = currentUserService;
        }

        [HttpGet("librarians")]
        [Description("Get all librarians")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLibrarians()
        {
            var librarians = await _userService.GetAllLibrariansAsync();
            return Ok(librarians);
        }

        [HttpGet("readers")]
        [Description("Get all readers")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> GetReaders()
        {
            var readers = await _userService.GetAllReadersAsync();
            return Ok(readers);
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
    }
}
