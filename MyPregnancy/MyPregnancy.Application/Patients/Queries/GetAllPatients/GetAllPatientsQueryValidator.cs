namespace MyPregnancy.Application.Patients.Queries.GetAllPatients
{
    using FluentValidation;
    using MyPregnancy.Application.Patients.Queries.GetAllPatients;

    public class GetAllPatientsQueryValidator : AbstractValidator<GetAllPatientsQuery>
    {
        public GetAllPatientsQueryValidator()
        {
            RuleFor(s => s.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(s => s.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
