using eBookSaleProject.Models;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eBookSaleProject.Data.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext appDbContext;

        public AuthorService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddAsync(Author author)
        {
            await appDbContext.Authors.AddAsync(author);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
          var result =  await appDbContext.Authors.FirstOrDefaultAsync(n => n.Id == id);
             appDbContext.Authors.Remove(result);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        { 
            return await appDbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await appDbContext.Authors.FirstOrDefaultAsync(n=>n.Id == id);
        }

        public async Task<Author> UpdateAsync(int id, Author newAuthor)
        {
            appDbContext.Authors.Update(newAuthor);
            await appDbContext.SaveChangesAsync();
            return newAuthor;
        }
    }
}
