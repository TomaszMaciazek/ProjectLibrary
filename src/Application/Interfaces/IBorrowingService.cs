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
        Task AddProlongRequestAsync(AddProlongRequestVM model);
        Task ChangeProlongRequestStatusAsync(UpdateProlongRequestVM requestVM);
        Task DeleteBorrowingAsync(int id);
        Task DeleteProlongRequestAsync(int id);
        Task FinishBorrowingAsync(UpdateBorrowingVM borrowingVM);
        Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllNotReturnedBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId);
        Task<ICollection<BorrowingDto>> GetAllUserNotReturnedBorrowingsAsync(int userId);
    }
}