namespace MyPregnancy.WebApi
{
    using Application;
    using Application.Patients.Queries.GetPatient;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyPregnancy.Application.Infrastructure;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using MyPregnancy.TaxCalculators;
    using MyPregnancy.WebApi.Middleware;
    using Persistence;
    using System.Reflection;
    using Microsoft.Extensions.Hosting;
    using MyPregnancy.Infrastructure;
    using MyPregnancy.Infrastructure.Clients.Models;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterPersistenceServices(Configuration);
            services.RegisterApplicationServices();
            services.RegisterTaxCalculatorServices();
            services.RegisterInfrastructureServices();

            services.AddMediatR(typeof(GetPatientQueryHandler).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient<IValidator<GetPatientQuery>, GetPatientQueryValidator>();

            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "MyPregnancy API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                };
            });

            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientCommandValidator>());

            services.Configure<CalculatorClientConfiguration>(Configuration.GetSection("CalculatorClientConfiguration"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            InitializeDatabase(app);

            app.UseHttpsRedirection();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<MyPregnancyDbContext>().Database.Migrate();
        }
    }
}
