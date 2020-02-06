namespace MyPregnancy.TaxCalculators.TaxCalculators
{
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;
    using System.Threading.Tasks;

    public class SelfEmployedTaxCalculator : ITaxCalculator
    {
        private readonly ILogger _logger;
        private readonly ICalculatorClient _calculatorClient;

        public Enums.Calculator Calculator => Enums.Calculator.SelfEmployed;

        public SelfEmployedTaxCalculator(ILogger<SelfEmployedTaxCalculator> logger, ICalculatorClient calculatorClient)
        {
            _logger = logger;
            _calculatorClient = calculatorClient;
        }

        public async Task<TaxCalculationSummary> CalculateTax()
        {
            _logger.LogInformation($"Entering {nameof(CalculateTax)}");

            var addResult = await _calculatorClient.Add(9, 9);

            _logger.LogInformation($"Calculator Add returned a value of {addResult}");

            return new TaxCalculationSummary { CalculatorName = nameof(SelfEmployedTaxCalculator), TotalTax = addResult };
        }
    }
}
