namespace MyPregnancy.TaxCalculators
{
    using Microsoft.Extensions.DependencyInjection;
    using MyPregnancy.TaxCalculators.Interfaces;
    using MyPregnancy.TaxCalculators.TaxCalculators;

    public static class ServicesCollectionExtension
    {
        public static void RegisterTaxCalculatorServices(this IServiceCollection services)
        {
            services.AddTransient<ITaxCalculatorFactory, TaxCalculatorFactory>();
            services.AddTransient<ITaxCalculator, SelfEmployedTaxCalculator>();
            services.AddTransient<ITaxCalculator, EmployedTaxCalculator>();

        }
    }
}
