using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class BookWithDetalisDto : BaseBookDto
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
