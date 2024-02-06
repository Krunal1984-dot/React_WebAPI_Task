using Microsoft.Extensions.Logging;
using Store.Repository;
using System;
using System.Data;

namespace Store.BusinessLayer
{
    public class BaseRespository<T> where T : class
    {
        protected readonly ILogger logger;
        protected readonly IRepository<T> repository;
        public BaseRespository(IDbConnection connString, ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = new Repository<T>(connString);
        }
    }
}
