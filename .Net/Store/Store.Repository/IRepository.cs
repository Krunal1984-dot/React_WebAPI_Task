using Dapper;
using System.Data;

namespace Store.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> BulkInsertAsync(List<T> entity);
        Task<List<T>> GetAllAsync(string sp, DynamicParameters? parameters, CommandType commandType = CommandType.StoredProcedure);
    }
}
