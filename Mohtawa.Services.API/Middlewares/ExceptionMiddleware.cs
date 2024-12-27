using Mohtawa.Services.Application.Interfaces;
using Mohtawa.Services.Application.Models.Responses;
using Serilog.Context;
using System.Net;

namespace Mohtawa.Services.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, IRequestInfoService requestInfoService)
        {
            try
            {
                SetLoggerProperties(requestInfoService);
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured in {httpContext.Request.Path}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new BaseResponse()
            {
                HttpStatusCode = (HttpStatusCode)context.Response.StatusCode,
                ErrorMessage = ex.InnerException?.Message ?? ex.Message,
            }.ToString());
        }

        private void SetLoggerProperties(IRequestInfoService requestInfoService)
        {
            if (!string.IsNullOrEmpty(requestInfoService.CorrelationId))
            {
                LogContext.PushProperty("CorrelationId", $"CorrelationId: {requestInfoService.CorrelationId}");
            }
        }
    }
}
