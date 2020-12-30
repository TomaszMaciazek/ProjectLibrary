using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IPublisherRepository
    {
        IEnumerable<Publisher> GetAllPublishers();
        Task<Publisher> GetPublisherById(int id);
    }
}