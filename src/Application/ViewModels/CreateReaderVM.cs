using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class CreateReaderVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public int? ReservationsLimit { get; set; }
        public int? BorrowingsLimit { get; set; }
    }
}
