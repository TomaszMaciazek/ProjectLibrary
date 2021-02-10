using System;

namespace Application.ViewModels.AddVM
{
    public class AddBorrowingVM : BaseAddVM
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
