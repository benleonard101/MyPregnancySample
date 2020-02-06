namespace MyPregnancy.TaxCalculators.Interfaces
{
    using System.Threading.Tasks;

    public interface ICalculatorClient
    {
        Task<int> Add(int a, int b);
    }
}
