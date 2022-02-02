using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eBookSaleProject.Data.Base
{
    public interface IEntityBaseRepository<T>where T : class, IEntityBase,new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
