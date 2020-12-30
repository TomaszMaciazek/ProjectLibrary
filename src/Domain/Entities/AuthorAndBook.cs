using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AuthorAndBook : BaseEntity
    {
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
