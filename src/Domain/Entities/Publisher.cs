using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Publisher : AuditEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
