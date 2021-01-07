using System;

namespace Application.ViewModels.AddVM
{
    public class AddReservationVM : BaseAddVM
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
