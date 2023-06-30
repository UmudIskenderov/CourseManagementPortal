using CourseManagementPortalWebUI.Exceptions;
using NLog;
using System.Net;

namespace CourseManagementPortalWebUI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next,
                                      ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException appException)
            {
                _logger.LogWarning(appException, "Error occured while processing request");

                await HandleErrorAsync(context, $"{appException.Message} \n {HttpStatusCode.BadRequest} : {(int)HttpStatusCode.BadRequest}");
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error occured while processing request");

                await HandleErrorAsync(context, $"{HttpStatusCode.InternalServerError} : {(int)HttpStatusCode.InternalServerError}");
            }
        }

        private async Task HandleErrorAsync(HttpContext context, string apiResponse)
        {
            var response = context.Response;
                        
            await response.WriteAsync(apiResponse);
        }
    }
}