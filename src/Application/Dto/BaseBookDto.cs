using Application.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class BaseBookDto : EditableAuditDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int YearOfRelease { get; set; }
        [Required]
        public int Count { get; set; }
        public PublisherDto Publisher { get; set; }
        public CategoryDto Category { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
