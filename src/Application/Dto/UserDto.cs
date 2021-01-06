using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public int BorrowingsLimit { get; set; }
        public int ReservationsLimit { get; set; }
    }
}
