# CleanArchTemplate

This solution was created following the guidelines of the Clean Architecture pattern as demonstrated by Jason Taylor in his Clean Architecture talks and posts. While the solution starts there, there are some opinionated coding patterns added in on top of the architecture that help to ease testing and maintenance.

## Original Clean Architecture Resources

-   [YouTube - Clean Architecture with ASP.NET Core 3.0 • Jason Taylor • GOTO 2019](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
-   [Blog Post - Clean Architecture with .NET Core: Getting Started](https://jasontaylor.dev/clean-architecture-getting-started/)

## Architecture

The solution is a set of .NET 5 C# projects. Each project has nullable type checking enabled out of the box. The projects implement the Clean Architecture pattern as described here:

-   _CleanArchTemplate.Domain_ - This project is meant to hold the domain entities of the application. The classes in the `Entities` folder map to the data tables or records in whatever data store the application is configured to use.
-   _CleanArchTemplate.Application_ - This project contains the business logic and rules that make the application as a whole run as it's supposed to. Any interfaces necessary for the application to run are defined in the `Interfaces` folder.
-   _CleanArchTemplate.Infrastructure_ - This project contains the implemenation of any logic that needs to communicate with outside entities, such as a database, the file system, other HTTP API's, and so forth.
-   _CleanArchTemplate.Api_ - This is the front-end of the application, and provides the start-up code and the API endpoint entry points. It is an ASP<nowiki/>.NET Web API project.

### Data Services

The architecture utilizes data services to manage access to the data store. Each data service is meant to isolate a single domain action within the service; retrieving a record and its related entities, or persisting them. For instance, the data store operation to get all of the people in the data store (as implemented by the sample `GetPeopleQuery.Handler.cs` file) is self contained in the data service `IGetPeopleDataService`.

The `AddInfrastructure` method will scan the `CleanArchTemplate.Application` project for all interfaces that start with `I`, and end with `DataSevice`, and will pair them with any classes in the `CleanArchTemplate.Infrastructure` project that implement the interfaces. So, if you have a data source that stores pets, you can create an interface called `IGetPetsDataService`, create an implementation class for it, and the `AddInfrastructure` method will pick it up and add it to the dependency injection container.

It is important to note that you do not need to register these data services directly (with Scoped lifetime). The `AddInfrastructure` method uses the following reflection code to automatically do this:

```C#
private static readonly Regex InterfacePattern = new Regex("I(?:.+)DataService", RegexOptions.Compiled);
...
(from c in typeof(Application.DependencyInjection).Assembly.GetTypes()
 where c.IsInterface && InterfacePattern.IsMatch(c.Name)
 from i in typeof(Infrastructure.DependencyInjection).Assembly.GetTypes()
 where c.IsAssignableFrom(i)
 select new
 {
     Contract = c,
     Implementation = i
 }).ToList()
.ForEach(x => services.AddScoped(x.Contract, x.Implementation));
```

### Endpoints

Endpoints work with a class called `EndpointResult`. This class is separate from the `ActionResults` used by ASP<nowiki/>.NET, and provides for a separation of concerns from the API itself. When the call to `IMediator.Send` returns, it is cast to an `ActionResult` via the `ToActionResult` extension method.

### Nuget Dependencies

#### MediatR

The solution uses the [MediatR](https://github.com/jbogard/MediatR) library from Jimmy Bogard to segregate the business logic of handling an HTTP request from the ASP<nowiki/>.NET API controllers. The logic for any single HTTP endpoint is relegated to a handler, and the controller logic simply calls `IMediator.Send` when it receives a request. This helps to enforce single responsibility, and improves testability. Each endpoint handlers takes in ONLY the dependencies that IT needs, as opposed to a traditional API controller that must take in ALL dependencies that are required by ALL of its endpoints.

#### AutoMapper

[AutoMapper](https://github.com/AutoMapper/AutoMapper), another popular library written by Jimmy Bogard, is also used to map to and from the various types in the application. The API endpoints should be written so that they only ever return or take in a value type or a view model record/class. This is done to hide the ultimate details about the back end data store, and also allows for the back end data model to change without necessarily having to change the exposed endpoint.

#### API Versioning

The API project uses the [ASP.NET API Versioning](https://github.com/dotnet/aspnet-api-versioning) library. This allows the HTTP endpoints to be versioned as changes occur.

#### Swashbuckle

[Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) is included to provide for Swagger documentation to be made available from the API. You can access the SwaggerDoc page by visiting `/swagger/v1/swagger.json` in your browser.

#### FluentValidation

The [FluentValidation](https://fluentvalidation.net/) library provides a fluid interface for defining validation rules on incoming view models. All input of an API should be considered suspect, and this library helps to ensure that incoming data is validated before being further processed.
---#if (includeDB)

#### Entity Framework

[Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) provides the data store access for the application.

---#endif
---#if (includePostgres)

#### Npgsql Entity Framework Core Provider

The [NpgSql Entity Framework Core Provider](https://www.npgsql.org/efcore/) is used to provide the necessary .NET drivers for connecting to a PostgreSQL database.

---#endif
