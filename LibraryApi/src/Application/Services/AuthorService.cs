﻿using Application.Dto;
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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<AuthorDto>> GetAllAuthorsAsync()
            => _mapper.Map<ICollection<AuthorDto>>(await _authorRepository.GetAllAuthorsAsync());

        public async Task<AuthorWithBooksDto> GetAuthorByIdAsync(int id)
            => _mapper.Map<AuthorWithBooksDto>(await _authorRepository.GetAuthorByIdAsync(id));

        public async Task<AuthorDto> AddAuthorAsync(AddAuthorVM author)
        {
            try
            {
                var mappedAuthor = _mapper.Map<Author>(author);
                await _authorRepository.AddAsync(mappedAuthor);
                return _mapper.Map<AuthorDto>(mappedAuthor);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }
        public async Task<bool> UpdateAuthorAsync(UpdateAuthorVM author)
        {
            try
            {
                var entity = await _authorRepository.GetAuthorByIdAsync(author.Id);
                if(entity == null)
                {
                    return false;
                }
                entity.Name = author.Name;
                entity.ModificationDate = author.ModyficationDate;
                entity.ModifiedBy = author.ModifiedBy;
                await _authorRepository.UpdateAsync(entity);
                return true;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            try
            {
                var author = await _authorRepository.GetAuthorByIdAsync(id);
                if(author == null)
                {
                    return false;
                }
                await _authorRepository.DeleteAsync(author);
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
