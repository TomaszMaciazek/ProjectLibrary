using System;

namespace Application.ViewModels.UpdateVM
{
    public class UpdateBorrowingVM : BaseUpdateVM
    {
        public DateTime? NewExpirationDate { get; set; }
        public DateTime? ReturnedByUser { get; set; }
    }
}
