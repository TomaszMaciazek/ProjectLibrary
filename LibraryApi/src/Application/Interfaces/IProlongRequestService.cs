using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProlongRequestService
    {
        Task AddProlongRequestAsync(AddProlongRequestVM model);
        Task DeleteProlongRequestAsync(int id);
        Task<ICollection<ProlongRequestDto>> GetAllAwaitingProlongRequestsAsync();
        Task<ICollection<ProlongRequestDto>> GetAllProlongRequestsAsync();
        Task<ProlongRequestDto> GetProlongRequestByIdAsync(int id);
        Task UpdateProlongRequestAsync(UpdateProlongRequestVM requestVM);
    }
}