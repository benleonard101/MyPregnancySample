namespace MyPregnancy.TaxCalculators.TaxCalculators
{
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;

    public class SelfEmployedTaxCalculator : ITaxCalculator
    {
        private readonly ILogger _logger;

        public Enums.Calculator Calculator => Enums.Calculator.SelfEmployed;

        public SelfEmployedTaxCalculator(ILogger<SelfEmployedTaxCalculator> logger)
        {
            _logger = logger;
        }

        public TaxCalculationSummary CalculateTax()
        {
            _logger.LogInformation($"Entering {nameof(CalculateTax)}");

            return new TaxCalculationSummary { CalculatorName = nameof(SelfEmployedTaxCalculator) };
        }
    }
}
