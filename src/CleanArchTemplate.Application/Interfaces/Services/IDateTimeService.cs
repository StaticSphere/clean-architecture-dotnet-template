using System;

namespace CleanArchTemplate.Application.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }
}