using Student.Shared.DomainModels;

namespace Student.Shared.Interfaces.Repositories
{
    public interface IDeleteRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// This will delete the object from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(T entity);

        Task DeleteRange(IEnumerable<T> entities);
    }
}
