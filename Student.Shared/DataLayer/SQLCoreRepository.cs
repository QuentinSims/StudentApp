using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Student.Shared.DataLayer
{
    public abstract class SQLCoreRepository
    {
        protected readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        protected readonly ILogger _logger;

        public SQLCoreRepository(IDbContextFactory<ApplicationDbContext > dbContextFactory, ILoggerFactory loggerFactory)
        {
            this.dbContextFactory = dbContextFactory;
            _logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        public ApplicationDbContext OpenContext()
        {
            return dbContextFactory.CreateDbContext();
        }
    }
}
