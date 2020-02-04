namespace MyPregnancy.Application.Patients.Queries.GetPatient
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MyPregnancy.Application.Exceptions;
    using MyPregnancy.Application.Interfaces;
    using MyPregnancy.Common.Dtos;
    using MyPregnancy.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientDto>
    {
        private readonly IMyPregnancyDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetPatientQueryHandler(IMyPregnancyDbContext context, IMapper mapper, ILogger<GetPatientQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(GetPatientQueryHandler)}");

            var patient = await _context
                .Patient.Where(p => p.PatientId == request.PatientId)
                .Include(x => x.MedicalDetail)
                .SingleOrDefaultAsync(cancellationToken);

            if (patient == null)
            {
                throw new NotFoundException(nameof(Patient), request.PatientId);
            }

            var patientDto = _mapper.Map<PatientDto>(patient);

            return patientDto;
        }
    }
}
