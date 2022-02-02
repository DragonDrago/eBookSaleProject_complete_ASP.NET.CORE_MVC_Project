using eBookSaleProject.Models;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eBookSaleProject.Data.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task<Author> UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);

    }
}
