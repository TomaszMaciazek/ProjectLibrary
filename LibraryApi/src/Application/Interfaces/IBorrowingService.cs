using Application.Args;
using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBorrowingService
    {
        Task<BorrowingDto> AddBorrowingAsync(AddBorrowingVM newBorrowing);
        Task<bool> DeleteBorrowingAsync(int id);
        Task<bool> UpdateBorrowingAsync(UpdateBorrowingVM borrowingVM);
        Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync(BorrowingsPaginationArgs args);
        Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId, BorrowingsPaginationArgs args);
        Task<BorrowingDto> GetBorrowingByIdAsync(int id);
    }
}