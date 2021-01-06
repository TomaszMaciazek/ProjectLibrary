using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class AddReservationDto : AuditDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
