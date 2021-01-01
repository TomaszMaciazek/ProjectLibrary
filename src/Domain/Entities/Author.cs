using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Author : AuditEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<AuthorAndBook> Books { get; set; }
    }
}
