using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Category : AuditEntity
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
