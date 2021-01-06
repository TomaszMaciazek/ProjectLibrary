using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IBorrowingService, BorrowingService>();
            return services;
        }
    }
}
