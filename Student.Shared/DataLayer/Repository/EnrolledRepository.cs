using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.Shared.DomainModels.CourseManagement;
using Student.Shared.Interfaces.Repositories;

namespace Student.Shared.DataLayer.Repository
{
    public class EnrolledRepository : SQLCRUD<EnrolledCourse>, IEnrolledCourseRepository
    {
        public EnrolledRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILoggerFactory logger) : base(dbContextFactory, logger)
        {

        }
    }
}
