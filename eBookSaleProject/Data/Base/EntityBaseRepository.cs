using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eBookSaleProject.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext appDbContext;

        public EntityBaseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public virtual async Task AddAsync(T entity)
        {
           await appDbContext.Set<T>().AddAsync(entity);
           await appDbContext.SaveChangesAsync();
        }

        public  async Task DeleteAsync(int id)
        {
            var entity = await appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            EntityEntry entityEntry = appDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();
        }

        public  async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await appDbContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = appDbContext.Set<T>();
            query = includeProperties.Aggregate(query,(current,includeProperties)=>current.Include(includeProperties));
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id) => await appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = appDbContext.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }


        public virtual async Task UpdateAsync( int id,T entity)
        {
           EntityEntry entityEntry = appDbContext.Entry<T>(entity);
           entityEntry.State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
        }
    }
}
