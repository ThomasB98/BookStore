using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message, Exception? exception = null)
        {
            if (exception == null)
            {
                _logger.LogError(message);
            }
            else
            {
                _logger.LogError(exception, message);
            }
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }

        public void LogCritical(string message, Exception? exception = null)
        {
            if (exception == null)
            {
                _logger.LogCritical(message);
            }
            else
            {
                _logger.LogCritical(exception, message);
            }
        }
    }
}
