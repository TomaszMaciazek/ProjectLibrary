using Application.Args;
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
        private readonly IMapper _mapper;

        public BorrowingService(
            IBorrowingRepository reservationRepository,
            IMapper mapper
            )
        {
            _borrowingRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<BorrowingDto>> GetAllBorrowingsAsync(BorrowingsPaginationArgs args)
        {
            try
            {
                return _mapper.Map<ICollection<BorrowingDto>>(
                        await _borrowingRepository.GetAllBorrowingsAsync(args.PageNumber, args.PageSize, args.OnlyNotReturned)
                    );
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<BorrowingDto> GetBorrowingByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<BorrowingDto>(await _borrowingRepository.GetBorrowingByIdAsync(id));
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }

        public async Task<BorrowingDto> AddBorrowingAsync(AddBorrowingVM newBorrowing)
        {
            try
            {
                var mappedBorrowing = _mapper.Map<Borrowing>(newBorrowing);
                await _borrowingRepository.AddAsync(mappedBorrowing);
                return _mapper.Map<BorrowingDto>(mappedBorrowing);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }

        }

        public async Task<bool> UpdateBorrowingAsync(UpdateBorrowingVM borrowingVM)
        {
            try
            {
                var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(borrowingVM.Id);
                if(borrowing == null)
                {
                    return false;
                }
                borrowing.ReturnedByUser = borrowingVM.ReturnedByUser ?? borrowing.ReturnedByUser;
                borrowing.ExpirationDate = borrowingVM.NewExpirationDate ?? borrowing.ExpirationDate;
                borrowing.ModificationDate = borrowingVM.ModyficationDate;
                borrowing.ModifiedBy = borrowingVM.ModifiedBy;
                await _borrowingRepository.UpdateAsync(borrowing);
                return true;
            }
            catch (Exception)
            {
                throw new ReturningBorrowingOperationFaliedException();
            }
        }



        public async Task<bool> DeleteBorrowingAsync(int id)
        {
            try
            {
                var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
                if(borrowing == null)
                {
                    return false;
                }
                await _borrowingRepository.DeleteAsync(borrowing);
                return true;
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }

        public async Task<ICollection<BorrowingDto>> GetAllUserBorrowingsAsync(int userId, BorrowingsPaginationArgs args)
        {
            try
            {
                return _mapper
                    .Map<ICollection<BorrowingDto>>(
                        await _borrowingRepository.GetAllUserBorrowingsAsync(userId, args.PageNumber, args.PageSize, args.OnlyNotReturned)
                    );
            }
            catch (Exception)
            {
                throw new GetOperationFailedException();
            }
        }
    }
}
