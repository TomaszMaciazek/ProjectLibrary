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
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> Get()
        {
            var borrowings = await _borrowingService.GetAllBorrowingsAsync();
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
                await _borrowingService.UpdateBorrowingAsync(borrowingVM);
                return NoContent();
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
                await _borrowingService.DeleteBorrowingAsync(id);
                return Ok();
            }
            catch (DeleteOperationFailedException) {
                return Problem();
            }
        }
    }
}
 