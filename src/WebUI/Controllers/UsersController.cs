using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Description("Get all librarians")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Librarians()
        {
            var librarians = await _userService.GetAllLibrariansAsync();
            return View(librarians);
        }

        [HttpGet]
        [Description("Get all readers")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Readers()
        {
            var readers = await _userService.GetAllReadersAsync();
            return View(readers);
        }

        [HttpPost]
        [Description("Add librarian to database")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLibrarian([FromBody]CreateLibrarianVM librarianVM)
        {
            try
            {
                await _userService.CreateLibrarianAsync(librarianVM);
            }
            catch (AddOperationFailedException) {}
            return Ok();
        }

        [HttpPost]
        [Description("Add reader to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> CreateReader([FromBody] CreateReaderVM readerVM)
        {
            try
            {
                await _userService.CreateReaderAsync(readerVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }
    }
}
