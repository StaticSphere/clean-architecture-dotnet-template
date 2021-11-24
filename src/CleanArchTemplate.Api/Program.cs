#if (useStartup)
using CleanArchTemplate.Api;
#endif
#if (!useStartup)
using CleanArchTemplate.Api.Services;
using CleanArchTemplate.Application;
using CleanArchTemplate.Application.Interfaces.Services;
using CleanArchTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
#endif

#if (useStartup)
CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                // TODO: Remove this line if you want to return the Server header
                .ConfigureKestrel(config => config.AddServerHeader = false)
                .UseStartup<Startup>();
        });
#else
// Configure Services
var builder = WebApplication.CreateBuilder(args);

// Adds in Application dependencies
builder.Services.AddApplication(builder.Configuration);
// Adds in Infrastructure dependencies
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
});

builder.Services.AddScoped<IPrincipalService, PrincipalService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchTemplate.Api", Version = "v1" });
});

// Configure Application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchTemplate.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.Use(async (httpContext, next) =>
{
    var apiMode = httpContext.Request.Path.StartsWithSegments("/api");
    if (apiMode)
    {
        httpContext.Request.Headers[HeaderNames.XRequestedWith] = "XMLHttpRequest";
    }
    await next();
});

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

app.Run();
#endif
