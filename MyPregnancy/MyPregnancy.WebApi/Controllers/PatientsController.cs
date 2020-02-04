namespace MyPregnancy.WebApi.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using MyPregnancy.Application.Patients.Queries.GetAllPatients;
    using MyPregnancy.Application.Patients.Queries.GetPatient;
    using System.Threading.Tasks;
    using System.Linq;
    using FluentValidation.Results;
    using MyPregnancy.Application.Exceptions;

    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public PatientsController(IMediator mediator, ILogger<PatientsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetAllPatientsQuery query)
        {
            _logger.LogInformation($"Entering {nameof(Get)}");

            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation($"Entering {nameof(Get)}/{id}");

            GetPatientQuery query = new GetPatientQuery { PatientId = id };

            var validationErrors = new GetPatientQueryValidator().Validate(query).Errors;
            if (validationErrors.Any())
            {
                throw new ValidationException(validationErrors.ToList());
            }

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
        {
            _logger.LogInformation($"Entering {nameof(Create)}");

            return Ok(await _mediator.Send(command));
        }
    }
}
