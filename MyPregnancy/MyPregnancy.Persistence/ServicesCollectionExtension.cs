namespace MyPregnancy.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using MyPregnancy.Application.Interfaces;

    public static class ServicesCollectionExtension
    {
        public static void RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyPregnancyDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("MyPregnancyDbContext")));

            services.AddScoped<IMyPregnancyDbContext>(provider => provider.GetService<MyPregnancyDbContext>());
        }
    }
}
