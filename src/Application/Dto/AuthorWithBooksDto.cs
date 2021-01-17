using System.Collections.Generic;

namespace Application.Dto
{
    public class AuthorWithBooksDto : AuthorDto
    {
        public IEnumerable<BaseBookDto> Books { get; set; }
    }
}
