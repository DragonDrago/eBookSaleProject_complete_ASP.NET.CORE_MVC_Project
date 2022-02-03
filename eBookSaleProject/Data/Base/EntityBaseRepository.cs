using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eBookSaleProject.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext appDbContext;

        public EntityBaseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddAsync(T entity)
        {
           await appDbContext.Set<T>().AddAsync(entity);
           await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            EntityEntry entityEntry = appDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await appDbContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
           EntityEntry entityEntry = appDbContext.Entry<T>(entity);
           entityEntry.State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
        }
    }
}
