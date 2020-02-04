namespace MyPregnancy.Application.Patients.Queries.GetAllPatients
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MyPregnancy.Application.Extensions;
    using MyPregnancy.Application.Interfaces;
    using MyPregnancy.Common.Dtos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
    {
        private readonly IMyPregnancyDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAllPatientsQueryHandler(IMyPregnancyDbContext context, IMapper mapper, ILogger<GetAllPatientsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(GetAllPatientsQueryHandler)}");

            var patients = await _context.Patient
                                .Paginate(request)
                                .OrderBy(p => p.Surname)
                                .Include(md => md.MedicalDetail)
                                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PatientDto>>(patients);

        }
    }
}
