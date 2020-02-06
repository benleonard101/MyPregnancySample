namespace MyPregnancy.WebApi.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;

        public TaxCalculatorController(ILogger<TaxCalculatorController> logger, ITaxCalculatorFactory taxCalculatorFactory)
        {
            _logger = logger;
            _taxCalculatorFactory = taxCalculatorFactory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaxCalculation([FromQuery] TaxCalculationDto dto)
        {
            _logger.LogInformation($"Entering {nameof(GetTaxCalculation)}");

            var taxCalculator = _taxCalculatorFactory.CreateTaxCalculator(dto.CalculationType);

            var taxCalculationSummary = await taxCalculator.CalculateTax();

            return Ok(taxCalculationSummary);
        }
    }
}
