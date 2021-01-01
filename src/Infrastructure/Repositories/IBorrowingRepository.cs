using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBorrowingRepository : IRepository<Borrowing>
    {
        Task<ICollection<Borrowing>> GetAllBorrowings();
        Task<ICollection<Borrowing>> GetAllNotReturnedBorrowings();
        Task<ICollection<Borrowing>> GetAllUserBorrowings(int userId);
        Task<ICollection<Borrowing>> GetAllUserNotReturnedBorrowings(int userId);
        Task<Borrowing> GetBorrowingById(int id);
    }
}