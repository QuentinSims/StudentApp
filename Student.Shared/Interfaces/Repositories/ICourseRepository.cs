using Student.Shared.DomainModels.CourseManagement;

namespace Student.Shared.Interfaces.Repositories
{
    public interface ICourseRepository : ICreateReadUpdateRepository<Course>, IDeleteRepository<Course>
    {
    }
}
