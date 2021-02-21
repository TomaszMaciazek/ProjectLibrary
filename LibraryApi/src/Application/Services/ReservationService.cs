using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReservationDto>> GetAllReservationsAsync()
        {
            try
            {
                return _mapper.Map<ICollection<ReservationDto>>(await _reservationRepository.GetAllReservationsAsync());
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<ICollection<ReservationDto>> GetAllAwaitingReservationsAsync()
        {
            try
            {
                return _mapper.Map<ICollection<ReservationDto>>(await _reservationRepository.GetAllAwaitingReservationsAsync());
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<ReservationDto>(await _reservationRepository.GetReservationByIdAsync(id));
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<ReservationDto> AddReservationAsync(AddReservationVM reservationDto)
        {
            try
            {
                var mappedReservation = _mapper.Map<Reservation>(reservationDto);
                await _reservationRepository.AddAsync(mappedReservation);
                return _mapper.Map<ReservationDto>(mappedReservation);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task<bool> UpdateReservationAsync(UpdateReservationVM model)
        {
            try
            {
                var reservation = await _reservationRepository.GetReservationByIdAsync(model.Id);
                if(reservation == null)
                {
                    return false;
                }
                reservation.ReservationStatus = (StatusEnum)model.NewStatus;
                reservation.ModificationDate = model.ModyficationDate;
                reservation.ModifiedBy = model.ModifiedBy;
                await _reservationRepository.UpdateAsync(reservation);
                return true;
            }
            catch
            {
                throw new StatusChangingFailedException();
            }
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetReservationByIdAsync(id);
                if (reservation == null)
                {
                    return false;
                }
                await _reservationRepository.DeleteAsync(reservation);
                return true;
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }

        public async Task<ICollection<ReservationDto>> GetAllUserReservationsAsync(int userId)
        {
            try
            {
                return _mapper.Map<ICollection<ReservationDto>>(await _reservationRepository.GetAllUserReservationsAsync(userId));
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<ICollection<ReservationDto>> GetAllUserAwaitingReservationsAsync(int userId)
        {
            try
            {
                return _mapper.Map<ICollection<ReservationDto>>(await _reservationRepository.GetAllUserAwaitingReservationsAsync(userId));
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }
    }
}
