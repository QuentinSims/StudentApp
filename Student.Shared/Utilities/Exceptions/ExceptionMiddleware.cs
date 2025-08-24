using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Student.Shared.Utilities.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(GetType().FullName);
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == 403 && httpContext.Response.HasStarted)
            {
                return;
            }
            try
            {
                if (httpContext.Request.Headers.TryGetValue("x-correlation-id", out var correlationId) && !string.IsNullOrEmpty(correlationId))
                {
                    httpContext.Items["x-correlation-id"] = correlationId;
                }

                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                if (ex is InternalServerErrorProcessException)
                {
                    _logger.LogError($"Reason: {((InternalServerErrorProcessException)ex).Reason}");
                }
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorResponseDetails? errorDetails = ParseExceptionAndWriteErrorMessage(context, exception);

            if (errorDetails is not null)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorDetails.ToString());
            }
        }
        public static ErrorResponseDetails? ParseExceptionAndWriteErrorMessage(HttpContext context, Exception exception) => exception switch
        {
            AggregateException => new Func<ErrorResponseDetails>(() => { return ParseExceptionAndWriteErrorMessage(context, exception.InnerException); })(),
            InvalidOperationException => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };
            })(),
            BadRequestModelException => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };
            })(),
            UnauthorizedAccessException err => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };

            })(),
            NotFoundException err => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };

            })(),
            ForbiddenReadOnlyException err => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };

            })(),
            InternalServerErrorDatabaseException err => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };
            })(),
            InternalServerErrorProcessException err => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error."
                };
            })(),
            ArgumentException => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                };
            })(),
            Exception err => new Func<ErrorResponseDetails>(() =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ErrorResponseDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error."
                };
            })(),
        };
    }
}
