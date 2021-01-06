using Application.Common;
using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public static class DtoToEntityMaps
    {
        public static IMapperConfigurationExpression AddDtoToEntityMaps(this IMapperConfigurationExpression config)
        {

            config.CreateMap<AddOrUpdateBookDto, Book>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .ForMember(dest => dest.Borrowings, opt => opt.Ignore())
                    .ForMember(dest => dest.Reservations, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.Publisher, opt => opt.Ignore())
                    .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                    .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            config.CreateMap<AddReservationDto, Reservation>()
                .ForMember(dest => dest.ReservationStatus, opt => opt.MapFrom(src => StatusEnum.Awaiting))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore());

            config.CreateMap<AddOrUpdateCategoryDto, CategoryDto>()
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            return config;
        }
    }
}
