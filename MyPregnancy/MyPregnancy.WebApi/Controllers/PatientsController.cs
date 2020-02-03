using Microsoft.AspNetCore.Mvc;
using MyPregnancy.Application.Patients.Queries.GetAllPatients;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPregnancy.Application.Patients.Commands.CreatePatient;
using MyPregnancy.Application.Patients.Commands.DeletePatient;
using MyPregnancy.Application.Patients.Commands.UpdatePatient;
using MyPregnancy.Application.Patients.Queries.GetPatient;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MyPregnancy.WebApi.Controllers
{
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

            return Ok(await _mediator.Send(new GetPatientQuery { PatientId = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
        {
            _logger.LogInformation($"Entering {nameof(Create)}");

            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdatePatientCommand command)
        {
            _logger.LogInformation($"Entering {nameof(Update)}");

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Entering {nameof(Delete)}/{id}");

            await _mediator.Send(new DeletePatientCommand { PatientId = id });

            return NoContent();
        }
    }
}
