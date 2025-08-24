using Student.Shared.DataLayer.Repository;
using Student.Shared.Interfaces.Repositories;
using StudentAppApi.Interfaces.CourseManagement;
using StudentAppApi.Services.CourseManagement;

namespace StudentAppApi.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Register repositories
            services.AddSingleton<ICourseRepository, CourseRepository>();
            services.AddSingleton<IEnrolledCourseRepository, EnrolledRepository>();

            // Register services
            services.AddScoped<ICourseManagementService, CourseManagementService>();
        }
    }
}
