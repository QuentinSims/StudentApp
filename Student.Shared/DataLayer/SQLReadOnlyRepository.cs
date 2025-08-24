using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.Shared.DomainModels;
using Student.Shared.Interfaces.Repositories;
using Student.Shared.Utilities;
using Student.Shared.Utilities.Exceptions;

namespace Student.Shared.DataLayer
{
    public abstract class SQLReadOnlyRepository<T> : SQLCoreRepository, IReadOnlyRepository<T> where T : BaseEntity
    {

        public SQLReadOnlyRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILoggerFactory loggerFactory) : base(dbContextFactory, loggerFactory)
        {
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await OpenContext().Set<T>().ToListAsync();
        }
        public async Task<List<T>> GetAllByIDsAsync(List<Guid> ids)
        {
            return await OpenContext().Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public IQueryable<T> GetBaseQueryable()
        {
            return OpenContext().Set<T>();
        }

        public async Task<T> FindEntityAsync(Guid primaryKey)
        {
            return await FindExpectedEntityAsync(primaryKey, StringUtils.FormatName(typeof(T).Name));
        }
        public async Task<T> FindEntityAsync(Guid primaryKey, string entityDescription)
        {
            return await FindExpectedEntityAsync(primaryKey, entityDescription);
        }

        /// <summary>
        /// Finds the expected entity or throws an exception
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="entityDescription"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        private async Task<T> FindExpectedEntityAsync(Guid primaryKey, string entityDescription)
        {
            var entity = await GetSingleOrDefaultAsync(primaryKey);
            if (entity == null)
            {
                throw new NotFoundException($"{entityDescription} not found");
            }
            return entity;
        }

        public async Task<T?> GetSingleOrDefaultAsync(Guid primaryKey)
        {
            return await OpenContext().Set<T>().SingleOrDefaultAsync(x => x.Id == primaryKey);
        }

        public int GetRowCountOrDefault()
        {
            try
            {

                return OpenContext().Set<T>().Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
    }
}
