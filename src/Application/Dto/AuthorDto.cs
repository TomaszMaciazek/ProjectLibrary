﻿using Application.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class AuthorDto : EditableAuditDto
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<BaseBookDto> Books { get; set; }
    }
}
