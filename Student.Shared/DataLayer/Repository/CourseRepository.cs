using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.Shared.DomainModels.CourseManagement;
using Student.Shared.Interfaces.Repositories;

namespace Student.Shared.DataLayer.Repository
{
    public class CourseRepository : SQLCRUD<Course>, ICourseRepository
    {
        public CourseRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILoggerFactory logger) : base(dbContextFactory, logger)
        {

        }
    }
}
