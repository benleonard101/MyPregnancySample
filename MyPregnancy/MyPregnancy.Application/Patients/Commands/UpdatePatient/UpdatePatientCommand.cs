namespace MyPregnancy.Application.Patients.Commands.UpdatePatient
{
    using MediatR;
    using MyPregnancy.Common.Dtos;

    public class UpdatePatientCommand : BasePatientDto, IRequest<int>
    {
        public int PatientId { get; set; }
    }
}
