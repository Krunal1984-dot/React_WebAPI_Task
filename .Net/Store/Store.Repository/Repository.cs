using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace Store.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly IDbConnection dbConnection;
        public Repository(IDbConnection connString)
        {
            this.dbConnection = connString;
        }
        public async Task<int> InsertAsync(T entity)
        {
            return await this.dbConnection.InsertAsync<T>(entity);
        }
        public async Task<int> BulkInsertAsync(List<T> entity)
        {
            return await this.dbConnection.InsertAsync<List<T>>(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = await this.dbConnection.GetAllAsync<T>();
            return result.ToList();
        }
        public async Task<T> GetAsync(int id)
        {
            return await this.dbConnection.GetAsync<T>(id);
        }

        public async Task<List<T>> GetAllAsync(string sp, DynamicParameters? parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            var result = await dbConnection.QueryAsync<T>(sp, parameters, commandType: commandType);
            return result.ToList();
        }
    }
}

