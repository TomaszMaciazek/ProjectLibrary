using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? BorrowingsLimit { get; set; }
        public string CardNumber { get; set; }
        public IEnumerable<Borrowing> Borrowings { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
