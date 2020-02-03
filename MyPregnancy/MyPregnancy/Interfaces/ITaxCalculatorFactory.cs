namespace MyPregnancy.TaxCalculators.Interfaces
{
    using MyPregnancy.Common;

    public interface ITaxCalculatorFactory
    {
        ITaxCalculator CreateTaxCalculator(Enums.Calculator calculator);
    }
}
