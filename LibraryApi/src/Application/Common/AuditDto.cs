using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common
{
    public class AuditDto : BaseDto
    {
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
}
