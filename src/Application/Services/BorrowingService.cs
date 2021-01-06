using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;

        private readonly IMapper _mapper;
        public BorrowingService(IBorrowingRepository reservationRepository, IMapper mapper)
        {
            _borrowingRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync()
        {
            try
            {
                return _mapper.Map<ICollection<BorrowingDto>>(await _borrowingRepository.GetAllBorrowingsAsync());
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<ICollection<BorrowingDto>> GetAllNotReturnedBorrowingsAsync()
        {
            try
            {
                return _mapper.Map<ICollection<BorrowingDto>>(await _borrowingRepository.GetAllNotReturnedBorrowingsAsync());
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task AddBorrowingAsync(AddBorrowingDto newBorrowing)
        {
            try
            {
                var mappedBorrowing = _mapper.Map<Borrowing>(newBorrowing);
                await _borrowingRepository.AddAsync(mappedBorrowing);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task FinishBorrowingAsync(int id)
        {
            try
            {
                var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
                borrowing.ReturnedByUser = DateTime.Now;
                await _borrowingRepository.UpdateAsync(borrowing);
            }
            catch (Exception)
            {
                throw new ReturningBorrowingOperationFaliedException();
            }
        }

        public async Task DeleteBorrowingAsync(int id)
        {
            try
            {
                await _borrowingRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }

        public async Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId)
        {
            try
            {
                return _mapper.Map<ICollection<BorrowingDto>>(await _borrowingRepository.GetAllUserBorrowingsAsync(userId));
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<ICollection<BorrowingDto>> GetAllUserNotReturnedBorrowingsAsync(int userId)
        {
            try
            {
                return _mapper.Map<ICollection<BorrowingDto>>(await _borrowingRepository.GetAllUserNotReturnedBorrowingsAsync(userId));
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }
    }
}
