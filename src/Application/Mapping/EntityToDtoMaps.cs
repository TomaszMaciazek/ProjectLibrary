using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Identity;
using System.Linq;

namespace Application.Mapping
{
    public static class EntityToDtoMaps
    {
        public static IMapperConfigurationExpression AddEntityToDtoMaps(this IMapperConfigurationExpression config)
        {
            config.CreateMap<Book, BaseBookDto>()
                    .ForMember(dest => dest.Authors, opt =>
                        opt.MapFrom(src => src.Authors.Select(x => x.Author)));

            config.CreateMap<Book, BookWithDetalisDto>()
                    .ForMember(dest => dest.Authors, opt =>
                        opt.MapFrom(src => src.Authors.Select(x => x.Author)));

            config.CreateMap<Author, AuthorDto>();

            config.CreateMap<Publisher, PublisherDto>();

            config.CreateMap<Category, CategoryDto>();

            config.CreateMap<Reservation, ReservationDto>();

            config.CreateMap<Borrowing, BorrowingDto>()
                    .ForMember(dest => dest.UserCardNumber, opt =>
                        opt.MapFrom(src => src.User.CardNumber))
                    .ForMember(dest => dest.UserFirstAndLastName, opt =>
                        opt.MapFrom(src => src.User.Name));

            config.CreateMap<ApplicationUser, UserDto>();

            return config;
        }
    }
}
