using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBorrowingService
    {
        Task AddBorrowingAsync(AddBorrowingVM newBorrowing);
        Task DeleteBorrowingAsync(int id);
        Task UpdateBorrowingAsync(UpdateBorrowingVM borrowingVM);
        Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllNotReturnedBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId);
        Task<ICollection<BorrowingDto>> GetAllUserNotReturnedBorrowingsAsync(int userId);
        Task<BorrowingDto> GetBorrowingByIdAsync(int id);
    }
}