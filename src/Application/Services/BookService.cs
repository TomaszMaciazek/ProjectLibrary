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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorAndBookRepository _authorAndBookRepository;
        private readonly IMapper _mapper;

        public BookService(
            IBookRepository bookRepository,
            IAuthorAndBookRepository authorAndBookRepository,
            IMapper mapper
            )
        {
            _bookRepository = bookRepository;
            _authorAndBookRepository = authorAndBookRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<BaseBookDto>> GetAllBooksAsync(string filterString)
            => _mapper.Map<ICollection<BaseBookDto>>(await _bookRepository.GetAllBooksAsync(filterString));
        public async Task<ICollection<BaseBookDto>> GetAllAvailableBooksAsync(string filterString)
            => _mapper.Map<ICollection<BaseBookDto>>(await _bookRepository.GetAllAvailableBooksAsync(filterString));
        public async Task<BookWithDetalisDto> GetBookByIdAsync(int id)
            => _mapper.Map<BookWithDetalisDto>(await _bookRepository.GetBookByIdAsync(id));
        public async Task AddBookAsync(AddBookVM newBook)
        {
            try
            {
                var mappedNewBook = _mapper.Map<Book>(newBook);
                await _bookRepository.AddAsync(mappedNewBook);
                foreach (var id in newBook.AuthorsId)
                {
                    AuthorAndBook newRelation = new AuthorAndBook
                    {
                        BookId = mappedNewBook.Id,
                        AuthorId = id
                    };
                    await _authorAndBookRepository.AddAsync(newRelation);
                }
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }
        public async Task UpdateBookAsync(UpdateBookVM book)
        {
            try
            {
                var entity = await _bookRepository.GetBookByIdAsync(book.Id);
                entity.Title = book.Title;
                entity.PublisherId = book.PublisherId;
                entity.CategoryId = book.CategoryId;
                entity.Description = book.Description;
                entity.ModificationDate = book.ModyficationDate;
                entity.ModifiedBy = book.ModifiedBy;
                await _authorAndBookRepository.ChangeBookRelationsAsync(book.AuthorsId, entity.Id);
                await _bookRepository.UpdateAsync(entity);
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }
        public async Task DeleteBookAsync(int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
