using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class AuthorDto : AuditDto
    {
        [Required]
        public string Name { get; set; }
    }
}
