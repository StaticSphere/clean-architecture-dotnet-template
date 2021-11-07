using System;

namespace CleanArchTemplate.Application.Interfaces.Services
{
    // This interface exists to enable unit testing of log commands, since they are
    // extension methods on the ILogger interface.
    public interface ILoggerService<TCategoryName>
    {
        void LogInformation(string message);
        void LogError(Exception exception);
        void LogError(Exception exception, string message);
    }
}