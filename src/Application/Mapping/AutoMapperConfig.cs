using Application.Dto;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Mapping
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(config =>
            {
                config.CreateMap<Book, BookDto>()
                    .ForMember(dest => dest.Authors, opt => 
                        opt.MapFrom(src => src.Authors.Select(x => x.Author)));

                config.CreateMap<AddOrUpdateBookDto, Book>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .ForMember(dest => dest.Borrowings, opt => opt.Ignore())
                    .ForMember(dest => dest.Reservations, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.Publisher, opt => opt.Ignore());

                config.CreateMap<Author, AuthorDto>();


                config.CreateMap<Publisher, PublisherDto>();

                config.CreateMap<Category, CategoryDto>();

                config.CreateMap<Reservation, ReservationDto>();

                config.CreateMap<Borrowing, BorrowingDto>()
                    .ForMember(dest => dest.UserCardNumber, opt =>
                        opt.MapFrom(src => src.User.CardNumber))
                    .ForMember(dest => dest.UserFirstAndLastName, opt =>
                        opt.MapFrom(src => src.User.Name));
            })
            .CreateMapper();
    }
}
