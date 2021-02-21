using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public int BorrowingsLimit { get; set; }
        public int ReservationsLimit { get; set; }
    }
}
