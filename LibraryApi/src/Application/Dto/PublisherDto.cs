using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class PublisherDto : EditableAuditDto
    {
        [Required]
        public string Name { get; set; }
    }
}
