using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using CleanArchTemplate.Application.Interfaces.Services;

namespace CleanArchTemplate.Infrastructure.Services
{
    // This service exists to enable unit testing of log commands, since they are
    // extension methods on the ILogger interface.    
    [ExcludeFromCodeCoverage]
    public class LoggerService<TCategoryName> : ILoggerService<TCategoryName>
    {
        private readonly ILogger<TCategoryName> _logger;

        public LoggerService(ILogger<TCategoryName> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }

        public void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
}