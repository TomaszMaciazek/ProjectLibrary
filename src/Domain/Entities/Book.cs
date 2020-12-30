using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book : AuditEntity
    {
        public int ReleaseYear { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<AuthorAndBook> Authors { get; set; }
        public IEnumerable<Borrowing> Borrowings { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
