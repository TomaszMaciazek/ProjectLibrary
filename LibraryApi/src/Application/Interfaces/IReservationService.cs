using Application.Args;
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
        Task<bool> UpdateReservationAsync(UpdateReservationVM model);
        Task<bool> DeleteReservationAsync(int id);
        Task<ICollection<ReservationDto>> GetAllReservationsAsync(ReservationsPaginationArgs args);
        Task<ICollection<ReservationDto>> GetAllUserReservationsAsync(int userId, ReservationsPaginationArgs args);
        Task<ReservationDto> GetReservationByIdAsync(int id);
    }
}