using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Borrowing : AuditEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
