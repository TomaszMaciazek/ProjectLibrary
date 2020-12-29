using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Publisher : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
