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
        Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllNotReturnedBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId);
        Task<ICollection<BorrowingDto>> GetAllUserNotReturnedBorrowingsAsync(int userId);
        Task<BorrowingDto> GetBorrowingByIdAsync(int id);
    }
}