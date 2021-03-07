using Application.Args;
using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingsController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpGet]
        [Description("Get all borrowings")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> Get(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null,
            [FromQuery] bool onlyNotReturned = false
            )
        {
            var args = new BorrowingsPaginationArgs
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyNotReturned = onlyNotReturned
            };
            var borrowings = await _borrowingService.GetAllBorrowingsAsync(args);
            return Ok(borrowings);
        }

        [HttpGet("reader/{id}")]
        [Description("Get all reader borrowings")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetUserBorrowings(
            [FromRoute] int id,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null,
            [FromQuery] bool onlyNotReturned = false
            )
        {
            var args = new BorrowingsPaginationArgs
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyNotReturned = onlyNotReturned
            };
            var borrowings = await _borrowingService.GetAllUserBorrowingsAsync(id, args);
            return Ok(borrowings);
        }

        [HttpGet("{id}")]
        [Description("Get borrowing by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> Get([FromRoute] int id)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            return Ok(borrowing);
        }

        [HttpPost]
        [Description("Add borrowing to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Post([FromBody]AddBorrowingVM borrowingVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(borrowingVM);
            }
            try
            {
                var borrowing = await _borrowingService.AddBorrowingAsync(borrowingVM);
                return Created($"api/borrowings/{borrowing.Id}",null);
            }
            catch (AddOperationFailedException) {
                return Problem();
            }
        }
        [HttpPut]
        [Description("Update borrowing in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Put([FromBody]UpdateBorrowingVM borrowingVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(borrowingVM);
            }
            try
            {
                var result = await _borrowingService.UpdateBorrowingAsync(borrowingVM);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (UpdateOperationFailedException) {
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [Description("Delete borrowing from Database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _borrowingService.DeleteBorrowingAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
        }
    }
}
 