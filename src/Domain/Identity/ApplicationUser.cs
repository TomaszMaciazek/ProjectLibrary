using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? BorrowingsLimit { get; set; }
        public string CardNumber { get; set; }
    }
}
