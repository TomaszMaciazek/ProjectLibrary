using Application.Dto;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class BooksPageVM
    {
        public ICollection<CategoryDto> Categories { get; set; }
        public ICollection<PublisherDto> Publishers { get; set; }
        public ICollection<AuthorDto> Authors { get; set; }
        public ICollection<BaseBookDto> Books { get; set; }
    }
}
