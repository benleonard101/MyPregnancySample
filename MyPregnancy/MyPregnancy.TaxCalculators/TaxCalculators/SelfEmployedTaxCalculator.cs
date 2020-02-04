namespace MyPregnancy.TaxCalculators.TaxCalculators
{
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;

    public class SelfEmployedTaxCalculator : ITaxCalculator
    {
        public Enums.Calculator Calculator => Enums.Calculator.SelfEmployed;

        public TaxCalculationSummary CalculateTax()
        {
            return new TaxCalculationSummary { CalculatorName = nameof(SelfEmployedTaxCalculator) };
        }
    }
}
