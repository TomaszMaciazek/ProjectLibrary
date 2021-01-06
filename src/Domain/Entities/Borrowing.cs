using Domain.Common;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Borrowing : AuditEntity
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        public DateTime? ReturnedByUser { get; set; }
        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<ProlongRequest> ProlongRequests { get; set; }
    }
}
