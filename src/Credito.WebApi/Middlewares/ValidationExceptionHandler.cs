using System.Linq;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Credito.WebApi.Middlewares
{
    public static class ValidationExceptionHandler
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(ValidationExceptionHandler));

        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder app) =>
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionFeature.Error is ValidationException exception)
                {
                    _logger.Debug(exception, "A validation error ocurred.");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(
                        new 
                        {
                            Message = exception.Message,
                            Fields = exception.Errors
                                .GroupBy(x => x.PropertyName)
                                .Select(x => new 
                                            { 
                                                Name = x.Key, 
                                                ErrorMessage = x.Select(v => v.ErrorMessage) 
                                            })
                        });
                }
            }));
    }
}