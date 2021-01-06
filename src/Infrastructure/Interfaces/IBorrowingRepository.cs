using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBorrowingRepository : IRepository<Borrowing>
    {
        Task<ICollection<Borrowing>> GetAllBorrowingsAsync();
        Task<ICollection<Borrowing>> GetAllNotReturnedBorrowingsAsync();
        Task<ICollection<Borrowing>> GetAllUserBorrowingsAsync(int userId);
        Task<ICollection<Borrowing>> GetAllUserNotReturnedBorrowingsAsync(int userId);
        Task<Borrowing> GetBorrowingByIdAsync(int id);
    }
}