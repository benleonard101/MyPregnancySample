namespace MyPregnancy.Application.Patients.Commands.DeletePatient
{
    using MediatR;
    using MyPregnancy.Application.Interfaces;
    using MyPregnancy.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IMyPregnancyDbContext _context;

        public DeletePatientCommandHandler(IMyPregnancyDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            Patient patient = new Patient { PatientId = request.PatientId };

            _context.Patient.Remove(patient);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
