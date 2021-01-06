using Application.Common;
using System;

namespace Application.Dto
{
    public class BorrowingDto : EditableAuditDto
    {
        public BaseBookDto Book { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? ReturnedByUser { get; set; }
        public string UserFirstAndLastName { get; set; }
        public string UserCardNumber { get; set; }
    }
}
