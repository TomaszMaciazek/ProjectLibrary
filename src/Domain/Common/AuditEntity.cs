using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class AuditEntity : BaseEntity
    {
        [Required]
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
