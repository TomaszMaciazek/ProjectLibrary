using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<ICollection<Publisher>> GetAllPublishers();
        Task<Publisher> GetPublisherById(int id);
    }
}