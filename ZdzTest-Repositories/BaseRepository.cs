using System.Data;

using Dapper;

using ZdzTest_Models;

namespace ZdzTest_Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly IDbConnection _dbConnection;

        protected readonly string activeFilter = " AND Active = 1";
        protected readonly string name = typeof(T).Name;

        public BaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var sql = $"SELECT * FROM {name} WHERE Id = @Id" + activeFilter;
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {name} WHERE Active = 1";
            return await _dbConnection.QueryAsync<T>(sql);
        }

        public async Task<int> AddAsync(T entity)
        {
            var sql = $"INSERT INTO {name} ({string.Join(",", typeof(T).GetProperties().Select(p => p.Name))}) " +
                $"VALUES (@{string.Join(",@", typeof(T).GetProperties().Select(p => p.Name))})";
            return await _dbConnection.ExecuteAsync(sql, entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var sql = $"UPDATE {name} SET {string.Join(",", typeof(T).GetProperties().Select(p => $"{p.Name} = @{p.Name}"))} WHERE Id = @Id"
                + activeFilter;
            return await _dbConnection.ExecuteAsync(sql, entity);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = $"UPDATE {name} SET Active = 0 WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }

}
