using Microsoft.Extensions.DependencyInjection;
using MyPregnancy.Infrastructure.Clients;
using MyPregnancy.TaxCalculators.Interfaces;

namespace MyPregnancy.Infrastructure
{
    public static class ServicesCollectionExtension
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICalculatorClient, CalculatorClient>();
        }
    }
}
