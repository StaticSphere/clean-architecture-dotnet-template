using System.Diagnostics.CodeAnalysis;
using CleanArchTemplate.Application.Interfaces.Services;

namespace CleanArchTemplate.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
