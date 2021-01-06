using System;

namespace Application.Common
{
    public abstract class EditableAuditDto : AuditDto
    {
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
