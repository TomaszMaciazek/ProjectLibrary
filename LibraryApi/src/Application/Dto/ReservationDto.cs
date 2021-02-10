using Application.Common;

namespace Application.Dto
{
    public class ReservationDto : EditableAuditDto
    {
        public BaseBookDto Book { get; set; }
    }
}
