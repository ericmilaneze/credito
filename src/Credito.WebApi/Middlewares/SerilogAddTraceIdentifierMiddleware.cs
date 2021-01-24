using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Credito.WebApi.Middlewares
{
    public class SerilogAddTraceIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public SerilogAddTraceIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("TraceIdentifier", context.TraceIdentifier))
                await _next.Invoke(context);
        }
    }
}