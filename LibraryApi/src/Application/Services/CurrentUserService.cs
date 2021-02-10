using Application.Interfaces;

namespace Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
