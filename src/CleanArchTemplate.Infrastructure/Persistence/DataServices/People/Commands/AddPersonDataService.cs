using System.Threading;
using System.Threading.Tasks;
#if (includeDB)
using CleanArchTemplate.Application.Interfaces.Persistence;
#endif
using CleanArchTemplate.Application.Interfaces.Persistence.DataServices.People.Commands;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Infrastructure.Persistence.DataServices.People.Commands
{
    public class AddPersonDataService : IAddPersonDataService
    {
#if (includeDB)
        private readonly ICleanArchTemplateDbContext _dbContext;

        public AddPersonDataService(ICleanArchTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
#endif

        public async Task<Person> ExecuteAsync(Person person, CancellationToken cancellationToken = default)
        {
#if (includeDB)
            _dbContext.People.Add(person);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return person;
#else
            // TODO: Create add code.
            return await Task.Run(() =>
            {
                person.Id = 1;

                return person;
            });
#endif
        }
    }
}
