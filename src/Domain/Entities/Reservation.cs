using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Reservation : AuditEntity
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
