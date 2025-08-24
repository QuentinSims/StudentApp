using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Shared.Interfaces.Repositories
{  
    /// <summary>
   /// This class represents the generic use of the Repository for Read requests.
   /// </summary>
   /// <typeparam name="T"></typeparam>
    public interface IReadOnlyRepository<T>
    {
        /// <summary>
        /// this will list all the entries listed in the database for the table 
        /// </summary>
        /// <returns>T</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// this will get a single entry from the database
        /// </summary>
        /// <returns>List<T></returns>
        Task<T?> GetSingleOrDefaultAsync(Guid primaryKey);


        /// <summary>
        /// Finds the expected entity or throws an exception
        /// </summary>
        /// <returns>List<T></returns>
        /// <exception cref="NotFoundException"></exception>
        Task<T> FindEntityAsync(Guid primaryKey);
        /// <summary>
        /// Finds the expected entity or throws an exception
        /// </summary>
        /// <returns>List<T></returns>
        /// <exception cref="NotFoundException"></exception>
        Task<T> FindEntityAsync(Guid primaryKey, string entityDescription);

        /// <summary>
        /// this will get a subset of entries from the DB denoted by their ids
        /// </summary>
        /// <returns>List<T></returns>
        Task<List<T>> GetAllByIDsAsync(List<Guid> ids);

        /// <summary>
        /// returns the queryable object of the data
        /// </summary>
        /// <returns>IQueryable<T></returns>
        IQueryable<T> GetBaseQueryable();

        /// <summary>
        /// returns a integer representing the number of entries in DbSet<T>, 0 otherwise
        /// </summary>
        /// <returns></returns>
        int GetRowCountOrDefault();
    }
}
