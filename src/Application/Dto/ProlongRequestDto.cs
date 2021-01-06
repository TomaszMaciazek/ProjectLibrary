using Application.Common;
using System;

namespace Application.Dto
{
    public class ProlongRequestDto : EditableAuditDto
    {
        public BaseBookDto Book { get; set; }
        public UserDto User { get; set; }
        public DateTime NewExpirationDate { get; set; }
        public StatusEnum Status { get; set; }
    }
}
