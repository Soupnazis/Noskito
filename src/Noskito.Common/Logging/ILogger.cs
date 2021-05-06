using System;

namespace Noskito.Common.Logging
{
    public interface ILogger
    {
        void Debug(string message);
        void Information(string message);
        void Warning(string message);
        void Error(string message);
        void Error(string message, Exception exception);
    }
}