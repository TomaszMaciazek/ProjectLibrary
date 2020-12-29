using System;

namespace Domain.Common
{
    public abstract class AuditEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
