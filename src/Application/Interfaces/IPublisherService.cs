using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPublisherService
    {
        Task AddPublisherAsync(AddPublisherVM publisher);
        Task DeletePublisherAsync(int id);
        Task<ICollection<PublisherDto>> GetAllPublishersAsync();
        Task<PublisherDto> GetPublisherByIdAsync(int id);
        Task UpdatePublisherAsync(UpdatePublisherVM publisher);
    }
}