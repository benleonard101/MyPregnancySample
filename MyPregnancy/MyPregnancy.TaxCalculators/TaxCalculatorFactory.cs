namespace MyPregnancy.TaxCalculators
{
    using MyPregnancy.TaxCalculators.Interfaces;
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using MyPregnancy.TaxCalculators.Exceptions;
    using static MyPregnancy.Common.Enums;

    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TaxCalculatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITaxCalculator CreateTaxCalculator(Calculator calculator)
        {
            var taxCalculators = _serviceProvider.GetServices<ITaxCalculator>();

            try
            {
                return taxCalculators.Single(b => b.Calculator == calculator);
            }
            catch (InvalidOperationException exception)
            {
                throw new UnknownTaxCalculatorException(calculator, exception);
            }
        }
    }
}
