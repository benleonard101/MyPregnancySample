namespace MyPregnancy.Application.Patients.Commands.DeletePatient
{
    using MediatR;

    public class DeletePatientCommand : IRequest
    {
        public int PatientId { get; set; }
    }
}
