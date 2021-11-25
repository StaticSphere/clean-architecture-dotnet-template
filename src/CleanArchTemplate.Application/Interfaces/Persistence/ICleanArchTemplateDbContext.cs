using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchTemplate.Application.Interfaces.Persistence;

public interface ICleanArchTemplateDbContext
{
    DbSet<Person> People { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
