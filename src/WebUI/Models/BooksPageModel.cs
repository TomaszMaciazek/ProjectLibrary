using Application.Dto;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class BooksPageModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<PublisherDto> Publishers { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
        public IEnumerable<BaseBookDto> Books { get; set; }
    }
}
