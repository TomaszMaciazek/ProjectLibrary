using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;
        public PublisherService(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PublisherDto>> GetAllPublishersAsync()
            => _mapper.Map<ICollection<PublisherDto>>(await _publisherRepository.GetAllPublishersAsync());

        public async Task<PublisherWithBooksDto> GetPublisherByIdAsync(int id)
            => _mapper.Map<PublisherWithBooksDto>(await _publisherRepository.GetPublisherByIdAsync(id));

        public async Task<PublisherDto> AddPublisherAsync(AddPublisherVM publisher)
        {
            try
            {
                var mappedPublsiher = _mapper.Map<Publisher>(publisher);
                await _publisherRepository.AddAsync(mappedPublsiher);
                return _mapper.Map<PublisherDto>(mappedPublsiher);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }
        public async Task<bool> UpdatePublisherAsync(UpdatePublisherVM publisher)
        {
            try
            {
                var entity = await _publisherRepository.GetPublisherByIdAsync(publisher.Id);
                if(entity == null)
                {
                    return false;
                }
                entity.Name = publisher.Name;
                entity.ModificationDate = publisher.ModyficationDate;
                entity.ModifiedBy = publisher.ModifiedBy;
                await _publisherRepository.UpdateAsync(entity);
                return true;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            try
            {
                var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
                if(publisher == null)
                {
                    return false;
                }
                await _publisherRepository.DeleteAsync(publisher);
                return true;

            }
            catch (InvalidOperationException)
            {
                throw new DeleteIsForbiddenException();
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
