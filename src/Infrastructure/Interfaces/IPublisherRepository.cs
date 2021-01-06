using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<ICollection<Publisher>> GetAllPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
    }
}