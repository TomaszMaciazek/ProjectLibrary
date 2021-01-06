using Application.Common;
using System;

namespace Application.Dto
{
    public class AddProlongRequestDto : AuditDto
    {
        public int UserId { get; set; }
        public int BorrowingId { get; set; }
        public DateTime NewExpirationDate { get; set; }
    }
}
