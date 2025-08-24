namespace Student.Shared.Interfaces.Repositories
{
    /// <summary>
    /// This class represents the generic use of the Repository for CRUD requests.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICreateReadUpdateRepository<T> : IReadOnlyRepository<T>
    {
        /// <summary>
        /// This will create the object on the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(T entity);

        /// <summary>
        /// This will create a range of objects on the database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task CreateRange(IEnumerable<T> entities);

        /// <summary>
        /// This will update the object on the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(T entity);

        /// <summary>
        /// This will update a range of objects on the database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRange(IEnumerable<T> entities);
    }
}
