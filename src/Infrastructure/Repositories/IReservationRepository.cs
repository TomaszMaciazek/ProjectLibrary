using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<ICollection<Reservation>> GetAllAwaitingReservations();
        Task<ICollection<Reservation>> GetAllReservations();
        Task<ICollection<Reservation>> GetAllUserAwaitingReservations(int userId);
        Task<ICollection<Reservation>> GetAllUserReservations(int userId);
        Task<Reservation> GetReservationById(int id);
    }
}