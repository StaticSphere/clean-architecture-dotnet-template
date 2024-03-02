using CleanArchTemplate.Api.Services;
using CleanArchTemplate.Application;
using CleanArchTemplate.Application.Interfaces.Services;
using CleanArchTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace CleanArchTemplate.Api;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Configuration);

        // Adds in Application dependencies
        services.AddApplication(Configuration);
        // Adds in Infrastructure dependencies
        services.AddInfrastructure(Configuration);

        services.AddHttpContextAccessor();
        services.AddHealthChecks();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = ApiVersion.Default;
        });

        services.AddScoped<IPrincipalService, PrincipalService>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchTemplate.Api", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
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
    }
}
