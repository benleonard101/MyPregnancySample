using MediatR;
using MyPregnancy.Common.Dtos;

namespace MyPregnancy.Application.Patients.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<PatientDto>
    {
        public int PatientId { get; set; }
    }
}
