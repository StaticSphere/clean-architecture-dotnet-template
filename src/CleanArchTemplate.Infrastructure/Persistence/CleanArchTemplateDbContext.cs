using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CleanArchTemplate.Application.Interfaces.Persistence;
using CleanArchTemplate.Application.Interfaces.Services;
using CleanArchTemplate.Domain.Common;
using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchTemplate.Infrastructure.Persistence
{
    public class CleanArchTemplateDbContext : DbContext, ICleanArchTemplateDbContext
    {
        private readonly IPrincipalService _principalService;
        private readonly IDateTimeService _dateTimeService;

        public DbSet<Person> People { get; set; } = null!;

        public CleanArchTemplateDbContext(
            DbContextOptions options,
            IPrincipalService principalService,
            IDateTimeService dateTimeService) : base(options)
        {
            _principalService = principalService;
            _dateTimeService = dateTimeService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedBy = _principalService.UserId;
                        entity.Entity.CreatedOn = _dateTimeService.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Entity.ModifiedBy = _principalService.UserId;
                        entity.Entity.ModifiedOn = _dateTimeService.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.SeedData();
        }
    }
}
