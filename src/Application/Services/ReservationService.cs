﻿using Application.Dto;
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

        public async Task AddReservationAsync(AddReservationVM reservationDto)
        {
            try
            {
                var mappedReservation = _mapper.Map<Reservation>(reservationDto);
                await _reservationRepository.AddAsync(mappedReservation);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task ChangeReservationStatus(UpdateReservationVM model)
        {
            try
            {
                var reservation = await _reservationRepository.GetReservationByIdAsync(model.Id);
                reservation.ReservationStatus = (StatusEnum)model.NewStatus;
                reservation.ModificationDate = model.ModyficationDate;
                reservation.ModifiedBy = model.ModifiedBy;
                await _reservationRepository.UpdateAsync(reservation);
            }
            catch
            {
                throw new StatusChangingFailedException();
            }
        }

        public async Task DeleteReservationAsync(int id)
        {
            try
            {
                await _reservationRepository.DeleteAsync(id);
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
