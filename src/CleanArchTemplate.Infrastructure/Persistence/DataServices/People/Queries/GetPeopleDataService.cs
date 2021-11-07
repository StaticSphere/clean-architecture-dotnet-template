using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
#if (includeDB)
using CleanArchTemplate.Application.Interfaces.Persistence;
#endif
using CleanArchTemplate.Application.Interfaces.Persistence.DataServices.People.Queries;
using CleanArchTemplate.Domain.Entities;
#if (includeDB)
using Microsoft.EntityFrameworkCore;
#endif

namespace CleanArchTemplate.Infrastructure.Persistence.DataServices.People.Queries
{
    public class GetPeopleDataService : IGetPeopleDataService
    {
#if (includeDB)
        private readonly ICleanArchTemplateDbContext _dbContext;

        public GetPeopleDataService(ICleanArchTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
#endif

        public async Task<IEnumerable<Person>> ExecuteAsync(bool includeInactive, CancellationToken cancellationToken = default)
        {
#if (includeDB)
            return await _dbContext.People.Where(p => includeInactive || p.Active).ToListAsync(cancellationToken);
#else
            // TODO: Create get people code
            return await Task.Run(() => new List<Person>(new[] {
                new Person { Id = 1, FirstName = "Brandon", LastName = "Smith", Active = true },
                new Person { Id = 2, FirstName = "Allison", LastName = "Brown", Active = true },
                new Person { Id = 3, FirstName = "Patricia", LastName = "McDonald" }
            }).Where(p => includeInactive || p.Active));
#endif
        }
    }
}
