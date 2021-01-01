using Application.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class BookDto : AuditDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int YearOfRelease { get; set; }
        public PublisherDto Publisher { get; set; }
        public CategoryDto Category { get; set; }
        public BookStatus Status { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
