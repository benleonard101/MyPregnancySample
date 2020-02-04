namespace MyPregnancy.WebApi
{
    using Application;
    using Application.Patients.Queries.GetPatient;
    using FluentValidation.AspNetCore;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Application.Infrastructure;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using MyPregnancy.TaxCalculators;
    using MyPregnancy.WebApi.Middleware;
    using Persistence;
    using System.Reflection;
    using FluentValidation;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterPersistenceServices(Configuration);
            services.RegisterApplicationServices();
            services.RegisterTaxCalculatorServices();

            services.AddMediatR(typeof(GetPatientQueryHandler).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerDocument();

            services.AddMvc(option => option.EnableEndpointRouting = false).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientCommandValidator>());

            services.AddTransient<IValidator<GetPatientQuery>, GetPatientQueryValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<MyPregnancyDbContext>().Database.Migrate();
            }
        }
    }
}
