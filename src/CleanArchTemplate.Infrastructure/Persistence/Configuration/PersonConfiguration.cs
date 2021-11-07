using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchTemplate.Infrastructure.Persistence.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person");

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
