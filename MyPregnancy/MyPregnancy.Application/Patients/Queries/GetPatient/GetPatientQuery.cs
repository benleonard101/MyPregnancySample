namespace MyPregnancy.Application.Patients.Queries.GetPatient
{
    using MediatR;
    using MyPregnancy.Common.Dtos;

    public class GetPatientQuery : IRequest<PatientDto>
    {
        public int PatientId { get; set; }
    }
}
