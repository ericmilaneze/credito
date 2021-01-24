using System;
using Serilog;

namespace Credito.Framework.SerilogExtensions
{
    public static class ILoggerExtensions
    {
        public static void Debug(this ILogger logger, Exception exception) =>
            logger.Debug(exception, exception.Message);

        public static void Error(this ILogger logger, Exception exception) =>
            logger.Error(exception, exception.Message);
    }
}