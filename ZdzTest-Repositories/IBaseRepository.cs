using ZdzTest_Models;

namespace ZdzTest_Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(Guid id);
    }
}
