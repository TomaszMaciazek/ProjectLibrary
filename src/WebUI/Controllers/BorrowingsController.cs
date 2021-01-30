using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class BorrowingsController : Controller
    {
        private readonly IBorrowingService _borrowingService;
        public BorrowingsController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpGet]
        [Description("Get borrowings")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> Index(bool onlyNotReturned)
        {
            var borrowings = onlyNotReturned 
                ? await _borrowingService.GetAllNotReturnedBorrowingsAsync() : await _borrowingService.GetAllBorrowingsAsync();
            return View(borrowings);
        }

        [HttpPost]
        [Description("Add new borrowing to database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> CreateBorrrowing([FromBody] AddBorrowingVM borrowingVM)
        {
            try
            {
                await _borrowingService.AddBorrowingAsync(borrowingVM);
            }
            catch (AddOperationFailedException) { }
            return RedirectToAction("index");
        }

        [HttpPut]
        [Description("Update borrowing in database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> UpdateBorrowing([FromBody] UpdateBorrowingVM borrowingVM)
        {
            try
            {
                await _borrowingService.UpdateBorrowingAsync(borrowingVM);
            }
            catch (UpdateOperationFailedException) { }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Description("Delete borrowing from database")]
        [Authorize(Roles = "Admin, Librarian")]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            try
            {
                await _borrowingService.DeleteBorrowingAsync(id);
            }
            catch (DeleteOperationFailedException) { }
            catch (DeleteIsForbiddenException) { }
            return RedirectToAction("index");
        }
    }
}
