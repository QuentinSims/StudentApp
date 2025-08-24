using Microsoft.AspNetCore.Diagnostics;
using Student.Shared.Utilities.Exceptions;

namespace StudentAppApi.Configurations
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            ILogger logger2 = logger;
            app.UseExceptionHandler(delegate (IApplicationBuilder appError)
            {
                appError.Run(async delegate (HttpContext context)
                {
                    context.Response.ContentType = "application/json";
                    IExceptionHandlerFeature exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        logger2.LogError($"Something went wrong: {exceptionHandlerFeature.Error}");
                        await context.Response.WriteAsync(new ErrorResponseDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>(Array.Empty<object>());
        }
    }
}
