using Application.Common;
using Application.ViewModels.AddVM;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public static class ViewModelToEntityMaps
    {
        public static IMapperConfigurationExpression AddViewModelToEntityMaps(this IMapperConfigurationExpression config)
        {
            config.CreateMap<AddBookVM, Book>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .ForMember(dest => dest.Borrowings, opt => opt.Ignore())
                    .ForMember(dest => dest.Reservations, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.Publisher, opt => opt.Ignore())
                    .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                    .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            config.CreateMap<AddReservationVM, Reservation>()
                .ForMember(dest => dest.ReservationStatus, opt => opt.MapFrom(src => StatusEnum.Awaiting))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore());

            config.CreateMap<AddCategoryVM, Category>()
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            config.CreateMap<AddBorrowingVM, Borrowing>()
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnedByUser, opt => opt.Ignore())
                .ForMember(dest => dest.ProlongRequests, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            config.CreateMap<AddProlongRequestVM, ProlongRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StatusEnum.Awaiting))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Borrowing, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            config.CreateMap<AddAuthorVM, Author>()
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            config.CreateMap<AddPublisherVM, Publisher>()
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
            return config;
        }
    }
}
