namespace MyPregnancy.Application.Patients.Queries.GetPatient
{
    using FluentValidation;

    public class GetPatientQueryValidator : AbstractValidator<GetPatientQuery>
    {
        public GetPatientQueryValidator()
        {
            RuleFor(s => s.PatientId).GreaterThanOrEqualTo(1);
        }
    }
}
