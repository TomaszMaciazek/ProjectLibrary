using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Reservation : AuditEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
