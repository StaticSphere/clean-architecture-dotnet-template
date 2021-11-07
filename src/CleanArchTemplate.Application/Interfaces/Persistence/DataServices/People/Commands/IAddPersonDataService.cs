using System.Threading;
using System.Threading.Tasks;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.Interfaces.Persistence.DataServices.People.Commands
{
    public interface IAddPersonDataService
    {
        Task<Person> ExecuteAsync(Person person, CancellationToken cancellationToken = default);
    }
}
