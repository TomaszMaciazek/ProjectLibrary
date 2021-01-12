using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book : AuditEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int YearOfRelease { get; set; }
        [Required]
        public int Count { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<AuthorAndBook> Authors { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
