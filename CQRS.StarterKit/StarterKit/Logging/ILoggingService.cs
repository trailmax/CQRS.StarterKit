using System;

namespace StarterKit.Logging
{
    /// <summary>
    /// Logging service to wrap around logging framework of your choice
    /// </summary>
    public interface ILoggingService
    {
        void SetLoggerName(String name);
        void Info(String message, params object[] args);
        void Error(String message, params object[] args);
        void ErrorException(String message, Exception exception);
        void Debug(Exception exception);
        void Debug(String message, Exception exception);
        void Debug(String message, params object[] args);
        void Trace(String message, params object[] args);
        void Warn(String message, params object[] args);
    }
}