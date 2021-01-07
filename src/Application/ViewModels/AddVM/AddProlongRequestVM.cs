using System;

namespace Application.ViewModels.AddVM
{
    public class AddProlongRequestVM : BaseAddVM
    {
        public int UserId { get; set; }
        public int BorrowingId { get; set; }
        public DateTime NewExpirationDate { get; set; }
    }
}
