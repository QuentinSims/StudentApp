using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.Shared.DomainModels;
using Student.Shared.Interfaces.Repositories;

namespace Student.Shared.DataLayer
{
    public abstract class SQLCreateReadUpdateRepository<T> : SQLReadOnlyRepository<T>, ICreateReadUpdateRepository<T> where T : BaseEntity
    {

        protected SQLCreateReadUpdateRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILoggerFactory logger) : base(dbContextFactory, logger)
        {
        }

        public async Task Create(T entity)
        {
            var context = OpenContext();
            await CreateWithContext(entity, context);
        }
        private async Task CreateWithContext(T entity, DbContext context)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            _logger.LogInformation($"created {entity.GetType().Name} with id:{entity.Id} to value {entity}");
        }
        public async Task CreateRange(IEnumerable<T> entities)
        {
            var context = OpenContext();
            await CreateRangeWithContext(entities, context);
        }
        private async Task CreateRangeWithContext(IEnumerable<T> entities, DbContext context)
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
            foreach (T entity in entities)
            {
                _logger.LogInformation($"created {entity.GetType().Name} with id:{entity.Id} to value {entity}");
            }
            _logger.LogInformation($"created {entities.Count()} {typeof(T).Name}");
        }
        public async Task Update(T entity)
        {
            var context = OpenContext();
            await UpdateWithContext(entity, context);
        }
        private async Task UpdateWithContext(T entity, DbContext context)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            _logger.LogInformation($"updated {entity.GetType().Name} with id:{entity.Id} to value {entity}");
        }
        public async Task UpdateRange(IEnumerable<T> entities)
        {
            var context = OpenContext();
            await UpdateRangeWithContext(entities, context);
        }
        private async Task UpdateRangeWithContext(IEnumerable<T> entities, DbContext context)
        {
            context.UpdateRange(entities);
            await context.SaveChangesAsync();
            foreach (T entity in entities)
            {
                _logger.LogInformation($"updated {entity.GetType().Name} with id:{entity.Id} to value {entity}");
            }
            _logger.LogInformation($"updated {entities.Count()} {typeof(T).Name}");
        }
    }
}
