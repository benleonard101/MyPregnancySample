namespace MyPregnancy.Application.Patients.Commands.CreatePatient
{
    using MyPregnancy.Application.Interfaces;
    using AutoMapper;
    using MediatR;
    using MyPregnancy.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MyPregnancy.Common.Dtos;
    using Microsoft.Extensions.Logging;

    public class CreatePatientCommand : BasePatientDto, IRequest<int>
    {
        public class Handler : IRequestHandler<CreatePatientCommand, int>
        {
            private readonly IMyPregnancyDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;

            public Handler(IMyPregnancyDbContext context, IMapper mapper, ILogger<CreatePatientCommand> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Entering {nameof(Handle)}");

                var patient = _mapper.Map<Patient>(request);

                _context.Patient.Add(patient);

                return await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
