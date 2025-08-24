using Student.Shared.DataLayer.Repository;
using Student.Shared.Interfaces.Repositories;
using StudentAppApi.Interfaces.Authentication;
using StudentAppApi.Interfaces.CourseManagement;
using StudentAppApi.Services.Authenication;
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
            services.AddSingleton<ITokenRepository, TokenRepository>();


            // Register services
            services.AddScoped<ICourseManagementService, CourseManagementService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
