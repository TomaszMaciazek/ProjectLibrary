﻿using Domain.Entities;
using Infrastructure.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<ICollection<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
    }
}