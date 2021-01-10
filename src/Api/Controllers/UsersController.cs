using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        /*
        public async Task<IActionResult> ChangeCurrentUser(int id, string username, string email)
        {
            await Task.Run(() =>
            {
                _currerntUserService.Email = email;
                _currerntUserService.UserId = id;
                _currerntUserService.Username = username;
            });
            return Ok();
        }
        */
    }
}
