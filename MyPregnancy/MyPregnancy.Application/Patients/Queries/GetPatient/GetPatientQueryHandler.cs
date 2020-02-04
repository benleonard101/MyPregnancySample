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

    public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientDto>
    {
        private readonly IMyPregnancyDbContext _context;
        private readonly IMapper _mapper;

        public GetPatientQueryHandler(IMyPregnancyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
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
