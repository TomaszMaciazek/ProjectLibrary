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
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IProlongRequestRepository _prolongRequestRepository;
        private readonly IMapper _mapper;

        public BorrowingService(
            IBorrowingRepository reservationRepository,
            IProlongRequestRepository prolongRequestRepository,
            IMapper mapper
            )
        {
            _borrowingRepository = reservationRepository;
            _prolongRequestRepository = prolongRequestRepository;
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

        public async Task AddBorrowingAsync(AddBorrowingVM newBorrowing)
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

        public async Task FinishBorrowingAsync(UpdateBorrowingVM borrowingVM)
        {
            try
            {
                var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(borrowingVM.Id);
                borrowing.ReturnedByUser = borrowingVM.ReturnedByUser;
                borrowing.ModificationDate = borrowingVM.ModyficationDate;
                borrowing.ModifiedBy = borrowingVM.ModifiedBy;
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

        public async Task AddProlongRequestAsync(AddProlongRequestVM model)
        {
            try
            {
                var mappedRequest = _mapper.Map<ProlongRequest>(model);
                await _prolongRequestRepository.AddAsync(mappedRequest);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }

        public async Task ChangeProlongRequestStatusAsync(UpdateProlongRequestVM requestVM)
        {
            try
            {
                var request = await _prolongRequestRepository.GetProlongRequestByIdAsync(requestVM.Id);
                request.Status = (StatusEnum)requestVM.NewStatus;
                request.ModificationDate = requestVM.ModyficationDate;
                request.ModifiedBy = requestVM.ModifiedBy;
                await _prolongRequestRepository.UpdateAsync(request);
            }
            catch (Exception)
            {
                throw new StatusChangingFailedException();
            }
        }

        public async Task DeleteProlongRequestAsync(int id)
        {
            try
            {
                await _prolongRequestRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
