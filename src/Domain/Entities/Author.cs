using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Author : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
