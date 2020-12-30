using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class PublisherDto : AuditDto
    {
        [Required]
        public string Name { get; set; }
    }
}
