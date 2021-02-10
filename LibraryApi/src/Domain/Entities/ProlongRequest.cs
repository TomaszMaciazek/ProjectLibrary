using Domain.Common;
using Domain.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProlongRequest : AuditEntity
    {
        [Required]
        public int BorrowingId { get; set; }
        public Borrowing Borrowing { get; set; }
        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public DateTime NewExpirationDate { get; set; }
        public StatusEnum Status { get; set; }
    }
}
