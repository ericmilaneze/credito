using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Credito.WebApi.Filters
{
    public class LoggingActionFilter : ActionFilterAttribute, IActionFilter
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(LoggingActionFilter));

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Verbose("Easter egg found... This is just an example.");
        }
    }
}