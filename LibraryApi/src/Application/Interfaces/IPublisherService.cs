using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPublisherService
    {
        Task<PublisherDto> AddPublisherAsync(AddPublisherVM publisher);
        Task DeletePublisherAsync(int id);
        Task<ICollection<PublisherDto>> GetAllPublishersAsync();
        Task<PublisherWithBooksDto> GetPublisherByIdAsync(int id);
        Task UpdatePublisherAsync(UpdatePublisherVM publisher);
    }
}