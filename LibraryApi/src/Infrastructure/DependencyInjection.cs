using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorAndBookRepository, AuthorAndBookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IBorrowingRepository, BorrowingRepository>();
            services.AddTransient<IProlongRequestRepository, ProlongRequestRepository>();
            return services;
        }
    }
}
