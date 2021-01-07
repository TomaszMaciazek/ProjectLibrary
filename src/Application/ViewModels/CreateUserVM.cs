namespace Application.ViewModels
{
    public class CreateUserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public int? ReservationsLimit { get; set; }
        public int? BorrowingsLimit { get; set; }
    }
}
