using Application.Dto;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService
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

        public async Task<ICollection<BookDto>> GetAllBooks()
            => _mapper.Map<ICollection<BookDto>>(await _bookRepository.GetAllBooks());
        public async Task<ICollection<BookDto>> GetAllAvailableBooks()
            => _mapper.Map<ICollection<BookDto>>(await _bookRepository.GetAllAvailableBooks());
        public async Task<BookDto> GetBookById(int id)
            => _mapper.Map<BookDto>(await _bookRepository.GetBookById(id));
        public async Task AddBook(AddOrUpdateBookDto newBook)
        {
            try
            {
                var mappedNewBook = _mapper.Map<Book>(newBook);
                await _bookRepository.Add(mappedNewBook);
                foreach (var id in newBook.AuthorsId)
                {
                    AuthorAndBook newRelation = new AuthorAndBook
                    {
                        BookId = mappedNewBook.Id,
                        AuthorId = id
                    };
                    await _authorAndBookRepository.Add(newRelation);
                }
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }
        public async Task DeleteBook(int id)
        {
            try
            {
                await _bookRepository.Delete(id);
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
