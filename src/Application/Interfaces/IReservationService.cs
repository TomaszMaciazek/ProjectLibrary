using Application.Dto;
using Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationService
    {
        Task AddReservationAsync(AddReservationDto reservationDto);
        Task CancelReservationAsync(ChangeReservationStatusVM model);
        Task DeleteReservationAsync(int id);
        Task<ICollection<ReservationDto>> GetAllAwaitingReservationsAsync();
        Task<ICollection<ReservationDto>> GetAllReservationsAsync();
        Task<ICollection<ReservationDto>> GetAllUserAwaitingReservationsAsync(int userId);
        Task<ICollection<ReservationDto>> GetAllUserReservationsAsync(int userId);
        Task RealizeReservationAsync(ChangeReservationStatusVM model);
    }
}