namespace Student.Shared.Interfaces.Repositories
{
    /// <summary>
    /// This class represents the generic use of the Repository for CRUD requests.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepositoryDeleteOperations<T>
    {
        /// <summary>
        /// This will delete the object on the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(T entity);

        /// <summary>
        /// This will delete a range of objects on the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteRange(IEnumerable<T> entity);
    }
}
