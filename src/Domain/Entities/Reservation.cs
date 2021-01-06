using Domain.Common;
using Domain.Identity;
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
        public ApplicationUser User { get; set; }
        public StatusEnum ReservationStatus { get; set; }
    }
}
