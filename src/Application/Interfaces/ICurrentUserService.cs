namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; set; }
        string Email { get; set; }
        string Username { get; set; }
    }
}