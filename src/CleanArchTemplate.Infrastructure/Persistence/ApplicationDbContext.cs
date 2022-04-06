using System.Reflection;
using CleanArchTemplate.Application.Interfaces.Persistence;
using CleanArchTemplate.Application.Interfaces.Services;
using CleanArchTemplate.Domain.Common;
using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchTemplate.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IPrincipalService _principalService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IConfiguration _configuration;

    public DbSet<Person> People { get; set; } = null!;

    public ApplicationDbContext(
        DbContextOptions options,
        IPrincipalService principalService,
        IDateTimeService dateTimeService,
        IConfiguration configuration) : base(options)
    {
        _principalService = principalService;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

#if(includePostgres)
        optionsBuilder.UseNpgsql(
#else
        optionsBuilder.UseSqlServer(
#endif
            _configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsHistoryTable("__EFMigrationsHistory")
#if(includePostgres)
        ).UseSnakeCaseNamingConvention();
#else
        );
#endif
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.SeedData();
    }
}
