using Application.Args;
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

        public async Task<ICollection<BaseBookDto>> GetAllBooksAsync(BookFilterArgs args)
            => _mapper.Map<ICollection<BaseBookDto>>(
                    await _bookRepository.GetAllBooksAsync(
                        args.FilterTitleString,
                        args.Authors,
                        args.Categories,
                        args.Publishers,
                        args.OnlyAvailable
                        )
                );
        public async Task<BookWithDetalisDto> GetBookByIdAsync(int id)
            => _mapper.Map<BookWithDetalisDto>(await _bookRepository.GetBookByIdAsync(id));
        public async Task<BookWithDetalisDto> AddBookAsync(AddBookVM newBook)
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
                return _mapper.Map<BookWithDetalisDto>(mappedNewBook);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }
        public async Task<bool> UpdateBookAsync(UpdateBookVM book)
        {
            try
            {
                var entity = await _bookRepository.GetBookByIdAsync(book.Id);
                if(entity == null)
                {
                    return false;
                }
                entity.Title = book.Title;
                entity.PublisherId = book.PublisherId;
                entity.ImageUrl = book.ImageUrl;
                entity.CategoryId = book.CategoryId;
                entity.Description = book.Description;
                entity.ModificationDate = book.ModyficationDate;
                entity.ModifiedBy = book.ModifiedBy;
                await _authorAndBookRepository.ChangeBookRelationsAsync(book.AuthorsId, entity.Id);
                await _bookRepository.UpdateAsync(entity);
                return true;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                if(book == null)
                {
                    return false;
                }
                await _bookRepository.DeleteAsync(book);
                return true;
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
