using Application.Common;

namespace Application.Dto
{
    public class ReservationDto : AuditDto
    {
        public BookDto Book { get; set; }
    }
}
