namespace MyPregnancy.Application.Patients.Commands.CreatePatient
{
    using FluentValidation;

    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            // Personal Details
            RuleFor(x => x.Forenames).Length(1, 20).NotEmpty();
            RuleFor(x => x.Surname).Length(1, 20).NotEmpty();
            RuleFor(x => x.PreferredName).Length(1, 20).NotEmpty();
            RuleFor(x => x.Language).MaximumLength(40).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();

            // Contact Details
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(m => m.MobileTelephone).Length(1, 20).NotEmpty().When(m => string.IsNullOrEmpty(m.HomeTelephone));
            RuleFor(m => m.HomeTelephone).Length(1, 20).NotEmpty().When(m => string.IsNullOrEmpty(m.MobileTelephone));

            // Medical Details
            RuleFor(x => x.BloodGroup).IsInEnum().NotEmpty();
            RuleFor(x => x.HealthCareNumber).Length(1, 20).NotEmpty();
            //RuleFor(x => x.HospitalNumber).Length(1, 20).NotEmpty();
            RuleFor(x => x.KnownAllergies).Length(1, 60).NotEmpty();

            // Address
            RuleFor(x => x.AddressLine1).Length(1, 20).NotEmpty();
            RuleFor(x => x.AddressLine2).Length(1, 20).NotEmpty();
            RuleFor(x => x.County).MaximumLength(24);
            RuleFor(x => x.City).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Postcode).MaximumLength(10).NotEmpty();
        }
    }
}
