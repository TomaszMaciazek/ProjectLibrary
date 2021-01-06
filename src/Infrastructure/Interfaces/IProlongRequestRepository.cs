using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProlongRequestRepository
    {
        Task<ICollection<ProlongRequest>> GetAllAwaitingProlongRequestsAsync();
        Task<ICollection<ProlongRequest>> GetAllProlongRequestsAsync();
        Task<ICollection<ProlongRequest>> GetAllUserAwaitingProlongRequestsAsync(int userId);
        Task<ICollection<ProlongRequest>> GetAllUserProlongRequestsAsync(int userId);
        Task<ProlongRequest> GetProlongRequestByIdAsync(int id);
    }
}