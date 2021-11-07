using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.Interfaces.Persistence.DataServices.People.Queries
{
    public interface IGetPeopleDataService
    {
        Task<IEnumerable<Person>> ExecuteAsync(bool includeInactive, CancellationToken cancellationToken = default);
    }
}
