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
    public class ProlongRequestService : IProlongRequestService
    {
        private readonly IProlongRequestRepository _prolongRequestRepository;
        private readonly IMapper _mapper;
        public ProlongRequestService(IProlongRequestRepository prolongRequestRepository, IMapper mapper)
        {
            _prolongRequestRepository = prolongRequestRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ProlongRequestDto>> GetAllProlongRequestsAsync()
            => _mapper.Map<ICollection<ProlongRequestDto>>(await _prolongRequestRepository.GetAllProlongRequestsAsync());

        public async Task<ICollection<ProlongRequestDto>> GetAllAwaitingProlongRequestsAsync()
            => _mapper.Map<ICollection<ProlongRequestDto>>(await _prolongRequestRepository.GetAllAwaitingProlongRequestsAsync());

        public async Task<ProlongRequestDto> GetProlongRequestByIdAsync(int id)
            => _mapper.Map<ProlongRequestDto>(await _prolongRequestRepository.GetProlongRequestByIdAsync(id));

        public async Task<ProlongRequestDto> AddProlongRequestAsync(AddProlongRequestVM model)
        {
            try
            {
                var mappedRequest = _mapper.Map<ProlongRequest>(model);
                await _prolongRequestRepository.AddAsync(mappedRequest);
                return _mapper.Map<ProlongRequestDto>(mappedRequest);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }

        public async Task<bool> UpdateProlongRequestAsync(UpdateProlongRequestVM requestVM)
        {
            try
            {
                var request = await _prolongRequestRepository.GetProlongRequestByIdAsync(requestVM.Id);
                if(request == null)
                {
                    return false;
                }
                request.Status = (StatusEnum)requestVM.NewStatus;
                request.ModificationDate = requestVM.ModyficationDate;
                request.ModifiedBy = requestVM.ModifiedBy;
                await _prolongRequestRepository.UpdateAsync(request);
                return true;
            }
            catch (Exception)
            {
                throw new StatusChangingFailedException();
            }
        }

        public async Task<bool> DeleteProlongRequestAsync(int id)
        {
            try
            {
                var request = await _prolongRequestRepository.GetProlongRequestByIdAsync(id);
                if(request == null)
                {
                    return false;
                }
                await _prolongRequestRepository.DeleteAsync(request);
                return true;
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
