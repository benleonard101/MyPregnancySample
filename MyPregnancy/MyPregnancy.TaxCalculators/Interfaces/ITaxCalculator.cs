namespace MyPregnancy.TaxCalculators.Interfaces
{
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;

    public interface ITaxCalculator
    {
        Enums.Calculator Calculator { get; }

        TaxCalculationSummary CalculateTax();
    }
}
