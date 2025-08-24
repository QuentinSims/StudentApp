using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.Shared.DomainModels;
using Student.Shared.Interfaces.Repositories;

namespace Student.Shared.DataLayer
{
    public abstract class SQLCRUD<T> : SQLCreateReadUpdateRepository<T>, IBaseRepositoryDeleteOperations<T>, ICreateReadUpdateRepository<T> where T : BaseEntity
    {

        protected SQLCRUD(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILoggerFactory logger) : base(dbContextFactory, logger)
        {
        }

        public async Task Delete(T entity)
        {
            var context = OpenContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteRange(IEnumerable<T> entities)
        {
            var context = OpenContext();
            await DeleteRangeWithContext(entities, context);

        }

        private async Task DeleteRangeWithContext(IEnumerable<T> entities, DbContext context)
        {
            context.RemoveRange(entities);
            await context.SaveChangesAsync();
            foreach (T entity in entities)
            {
                _logger.LogInformation($"deleted {entity.GetType().Name} with id:{entity.Id} to value {entity}");
            }
            _logger.LogInformation($"deleted {entities.Count()} {typeof(T).Name}");
        }
    }
}
