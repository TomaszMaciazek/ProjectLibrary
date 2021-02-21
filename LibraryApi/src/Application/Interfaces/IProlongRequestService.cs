using Application.Dto;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProlongRequestService
    {
        Task<ProlongRequestDto> AddProlongRequestAsync(AddProlongRequestVM model);
        Task<bool> DeleteProlongRequestAsync(int id);
        Task<ICollection<ProlongRequestDto>> GetAllAwaitingProlongRequestsAsync();
        Task<ICollection<ProlongRequestDto>> GetAllProlongRequestsAsync();
        Task<ProlongRequestDto> GetProlongRequestByIdAsync(int id);
        Task<bool> UpdateProlongRequestAsync(UpdateProlongRequestVM requestVM);
    }
}