using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common
{
    public abstract class AuditDto : BaseDto
    {
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
