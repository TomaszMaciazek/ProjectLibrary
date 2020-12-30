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
        [Required]
        public string PublisherName { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public IEnumerable<string> AuthorsNames { get; set; }
    }
}
