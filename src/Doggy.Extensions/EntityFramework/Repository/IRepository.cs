using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Extensions.EntityFramework.Entity;

namespace Doggy.Extensions.EntityFramework.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(int id);
    }
}