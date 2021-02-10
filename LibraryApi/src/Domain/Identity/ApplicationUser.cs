using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? BorrowingsLimit { get; set; }
        public int? ReservationsLimit { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ProlongRequest> ProlongRequests { get; set; }
    }
}
