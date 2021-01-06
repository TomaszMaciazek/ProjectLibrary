using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<ICollection<Reservation>> GetAllAwaitingReservationsAsync();
        Task<ICollection<Reservation>> GetAllReservationsAsync();
        Task<ICollection<Reservation>> GetAllUserAwaitingReservationsAsync(int userId);
        Task<ICollection<Reservation>> GetAllUserReservationsAsync(int userId);
        Task<Reservation> GetReservationByIdAsync(int id);
    }
}