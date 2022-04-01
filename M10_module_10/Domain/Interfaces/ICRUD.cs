using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICRUD<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T item);
        Task<bool> UpdateAsync(T item, int id);
        Task<bool> DeleteAsync(int id);
    }
}
