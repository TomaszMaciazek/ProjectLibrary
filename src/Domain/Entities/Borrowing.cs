using Domain.Common;
using Domain.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Borrowing : AuditEntity
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
