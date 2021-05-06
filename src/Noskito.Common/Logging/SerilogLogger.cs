using System;
using Serilog;
using Serilog.Core;

namespace Noskito.Common.Logging
{
    public sealed class SerilogLogger : ILogger
    {
        private readonly Logger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .CreateLogger();

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Information(string message)
        {
            logger.Information(message);
        }

        public void Warning(string message)
        {
            logger.Warning(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(exception, message);
        }
    }
}