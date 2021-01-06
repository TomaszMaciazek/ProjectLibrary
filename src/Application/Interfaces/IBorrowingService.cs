using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBorrowingService
    {
        Task AddBorrowingAsync(AddBorrowingDto newBorrowing);
        Task DeleteBorrowingAsync(int id);
        Task FinishBorrowingAsync(int id);
        Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllNotReturnedBorrowingsAsync();
        Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId);
        Task<ICollection<BorrowingDto>> GetAllUserNotReturnedBorrowingsAsync(int userId);
    }
}