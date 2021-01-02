using System.Net;
using Credito.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Credito.WebApi.Middlewares
{
    public static class ResourceAlreadyExistsExceptionHandler
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(ResourceAlreadyExistsExceptionHandler));

        public static IApplicationBuilder UseResourceAlreadyExistsExceptionHandler(this IApplicationBuilder app) =>
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionFeature.Error is ResourceAlreadyExistsException exception)
                {
                    _logger.Debug(exception, "The resource already exists.");
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    await context.Response.WriteAsJsonAsync(
                        new 
                        {
                            Message = exception.Message,
                            Request = exception.Request
                        });
                }
            }));
    }
}