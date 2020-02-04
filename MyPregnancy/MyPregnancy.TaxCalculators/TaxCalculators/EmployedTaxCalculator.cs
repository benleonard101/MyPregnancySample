namespace MyPregnancy.TaxCalculators.TaxCalculators
{
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;

    public class EmployedTaxCalculator : ITaxCalculator
    {
        public Enums.Calculator Calculator => Enums.Calculator.Employed;

        public TaxCalculationSummary CalculateTax()
        {
            return new TaxCalculationSummary { CalculatorName = nameof(EmployedTaxCalculator) };
        }
    }
}
