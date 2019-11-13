using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doggy.Learning.Infrastructure.Interfaces
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