namespace MyPregnancy.Application.Patients.Commands.UpdatePatient
{
    using MediatR;
    using Exceptions;
    using Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MyPregnancy.Application.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, int>
    {
        private readonly IMyPregnancyDbContext _context;

        public UpdatePatientCommandHandler(IMyPregnancyDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient entity = await _context.Patient.FindAsync(request.PatientId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Patient), request.PatientId);
            }

            _context.Entry(entity).CurrentValues.SetValues(request);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            return request.PatientId;
        }
    }
}
