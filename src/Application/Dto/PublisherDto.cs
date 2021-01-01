﻿using Application.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class PublisherDto : AuditDto
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
