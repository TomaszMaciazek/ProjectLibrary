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
        public async Task<ActionResult<ICollection<BorrowingDto>>> Get()
        {
            var borrowings = await _borrowingService.GetAllBorrowingsAsync();
            return Ok(borrowings);
        }

        [HttpGet("{id}")]
        [Description("Get borrowing by id")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<ActionResult<ICollection<BorrowingDto>>> Get(int id)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            return Ok(borrowing);
        }

        [HttpPost]
        [Description("Add borrowing to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Post([FromBody]AddBorrowingVM borrowingVM)
        {
            try
            {
                await _borrowingService.AddBorrowingAsync(borrowingVM);
            }
            catch (AddOperationFailedException) { }
            return Ok();
        }
        [HttpPut]
        [Description("Update borrowing in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Put([FromBody]UpdateBorrowingVM borrowingVM)
        {
            try
            {
                await _borrowingService.UpdateBorrowingAsync(borrowingVM);
            }
            catch (UpdateOperationFailedException) { }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Description("Delete borrowing from Database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _borrowingService.DeleteBorrowingAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            return Ok();
        }
    }
}
 