using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Logger
{
    public static class LoggerExtensions
    {
        public static void LogWithContext(
            this ILogger logger,
            LogLevel logLevel,
            string message,
            object? context = null)
        {
            if(context == null)
            {
                logger.Log(logLevel, message);
                return;
            }

            var contextType = context.GetType();
            logger.Log(
                logLevel,
                "Message: {Message}. Context: {ContextType} - {ContextDetails}",
                message,
                contextType.Name,
                System.Text.Json.JsonSerializer.Serialize(context)
            );
        }
    }
}
