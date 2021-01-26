using System.Net;
using Credito.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using Credito.Framework.SerilogExtensions;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using System;
using Credito.WebApi.Models;
using Newtonsoft.Json;

namespace Credito.WebApi.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(ExceptionHandlerMiddleware));

        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app) =>
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                switch (exceptionFeature.Error)
                {
                    case ValidationException exception:
                        await HandleException(context, exception);
                        break;
                    case ResourceNotFoundException exception:
                        await HandleException(context, exception);
                        break;
                    case ResourceAlreadyExistsException exception:
                        await HandleException(context, exception);
                        break;
                    default:
                        await HandleException(context, exceptionFeature.Error);
                        break;
                }
            }));

        private static async Task HandleException(HttpContext context, ValidationException exception)
        {
            _logger.Debug(exception);
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(
                new ValidationErrorModel
                {
                    Message = exception.Message,
                    Fields = exception.Errors
                        .GroupBy(x => x.PropertyName)
                        .Select(x => new ValidationErrorFieldModel(x.Key,
                                                                   x.Select(v => v.ErrorMessage))),
                    TraceIdentifier = context.TraceIdentifier
                });
            await context.Response.CompleteAsync();
        }

        private static async Task HandleException(HttpContext context, ResourceNotFoundException exception)
        {
            _logger.Debug(exception);
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await WriteResourceException(context, exception);
            await context.Response.CompleteAsync();
        }

        private static async Task WriteResourceException(HttpContext context, ResourceException exception) =>
            await context.Response.WriteAsJsonAsync(
                new ResourceErrorModel(exception.Message,
                                       JsonConvert.SerializeObject(exception.Request),
                                       context.TraceIdentifier));

        private static async Task HandleException(HttpContext context, ResourceAlreadyExistsException exception)
        {
            _logger.Debug(exception);
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await WriteResourceException(context, exception);
            await context.Response.CompleteAsync();
        }

        private static async Task HandleException(HttpContext context, Exception exception)
        {
            _logger.Error(exception);
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(
                new DefaultErrorModel("Internal Server Error",
                                      context.TraceIdentifier));
            await context.Response.CompleteAsync();
        }
    }
}