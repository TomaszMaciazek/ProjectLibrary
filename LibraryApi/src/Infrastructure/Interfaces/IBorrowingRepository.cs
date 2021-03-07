using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBorrowingRepository : IRepository<Borrowing>
    {
        Task<ICollection<Borrowing>> GetAllBorrowingsAsync(int? pageNumber, int? pageSize, bool onlyNotReturned);
        Task<ICollection<Borrowing>> GetAllUserBorrowingsAsync(int userId, int? pageNumber, int? pageSize, bool onlyNotReturned);
        Task<Borrowing> GetBorrowingByIdAsync(int id);
    }
}