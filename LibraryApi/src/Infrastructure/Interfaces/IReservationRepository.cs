using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<ICollection<Reservation>> GetAllReservationsAsync(int? pageNumber, int? pageSize, bool onlyAwaiting);
        Task<ICollection<Reservation>> GetAllUserReservationsAsync(int userId, int? pageNumber, int? pageSize, bool onlyAwaiting);
        Task<Reservation> GetReservationByIdAsync(int id);
    }
}