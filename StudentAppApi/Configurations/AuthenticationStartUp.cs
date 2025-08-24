using Microsoft.AspNetCore.Mvc.Infrastructure;
using StudentAppApi.Interfaces.Authentication;
using StudentAppApi.Services.Authenication;

namespace StudentAppApi.Configurations
{
    public static class AuthenticationStartUp
    {
        public static void ConfigureJwtAuthenticationClaimsService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUserClaimsService, UserClaimsService>();
        }
    }
}
