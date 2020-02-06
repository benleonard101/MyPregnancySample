namespace MyPregnancy.TaxCalculators.Interfaces
{
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;
    using System.Threading.Tasks;

    public interface ITaxCalculator
    {
        Enums.Calculator Calculator { get; }

        Task<TaxCalculationSummary> CalculateTax();
    }
}
