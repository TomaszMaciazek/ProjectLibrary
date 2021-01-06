namespace Application.ViewModels
{
    public class UpdateUserVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? ReservationsLimit { get; set; }
        public int? BorrowingsLimit { get; set; }
    }
}
