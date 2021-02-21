using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDto> AddReservationAsync(AddReservationVM reservationDto);
        Task UpdateReservationAsync(UpdateReservationVM model);
        Task DeleteReservationAsync(int id);
        Task<ICollection<ReservationDto>> GetAllAwaitingReservationsAsync();
        Task<ICollection<ReservationDto>> GetAllReservationsAsync();
        Task<ICollection<ReservationDto>> GetAllUserAwaitingReservationsAsync(int userId);
        Task<ICollection<ReservationDto>> GetAllUserReservationsAsync(int userId);
        Task<ReservationDto> GetReservationByIdAsync(int id);
    }
}