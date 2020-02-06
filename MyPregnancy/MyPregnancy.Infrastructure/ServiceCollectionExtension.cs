namespace MyPregnancy.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using MyPregnancy.Infrastructure.Clients;
    using MyPregnancy.TaxCalculators.Interfaces;

    public static class ServicesCollectionExtension
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ICalculatorClient, CalculatorClient>();
        }
    }
}
