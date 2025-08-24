using Student.Shared.DomainModels.CourseManagement;

namespace Student.Shared.Interfaces.Repositories
{
    public interface IEnrolledCourseRepository : ICreateReadUpdateRepository<EnrolledCourse>, IDeleteRepository<EnrolledCourse>
    {
    }
}
