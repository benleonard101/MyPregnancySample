namespace MyPregnancy.WebApi.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;

        public TaxCalculatorController(ILogger<PatientsController> logger, ITaxCalculatorFactory taxCalculatorFactory)
        {
            _logger = logger;
            _taxCalculatorFactory = taxCalculatorFactory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] TaxCalculationDto dto)
        {
            _logger.LogInformation($"Entering {nameof(Get)}");

            var taxCalculator = _taxCalculatorFactory.CreateTaxCalculator(dto.CalculationType);

            var taxCalculationSummary = taxCalculator.CalculateTax();

            return Ok(taxCalculationSummary);
        }
    }
}
