using Application.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class AddOrUpdateBookDto : AuditDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int YearOfRelease { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ICollection<int> AuthorsId { get; set; }
    }
}
