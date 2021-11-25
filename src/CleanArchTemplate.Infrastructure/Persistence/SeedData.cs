using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchTemplate.Infrastructure.Persistence;

public static class SeedDataExtension
{
    public static void SeedData(this ModelBuilder builder)
    {
        // TODO: Use this file to seed the database with any initial data that
        // should exist the first time the application is run.

        builder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "Brandon", LastName = "Smith", Active = true, CreatedBy = 1, CreatedOn = DateTime.UtcNow },
            new Person { Id = 2, FirstName = "Allison", LastName = "Brown", Active = true, CreatedBy = 1, CreatedOn = DateTime.UtcNow },
            new Person { Id = 3, FirstName = "Patricia", LastName = "McDonald", CreatedBy = 1, CreatedOn = DateTime.UtcNow }
        );
    }
}
