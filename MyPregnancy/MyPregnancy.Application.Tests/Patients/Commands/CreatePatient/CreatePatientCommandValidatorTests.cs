namespace MyPregnancy.Application.Tests.Patients.Commands.CreatePatient
{
    using NUnit.Framework;
    using FluentValidation.TestHelper;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using AutoFixture;

    public class CreatePatientCommandValidatorTests
    {
        [TestFixture]
        public class PersonValidatorTester
        {
            private CreatePatientCommandValidator validator;
            private Fixture fixture;

            [SetUp]
            public void Setup()
            {
                validator = new CreatePatientCommandValidator();
                fixture = new Fixture();
            }

            [Test]
            public void CreatePatientCommandValidator_NullProperties_FailsValidation()
            {
                CreatePatientCommand patientCommand = new CreatePatientCommand();

                validator.ShouldHaveValidationErrorFor(x => x.City, patientCommand)
                   .WithErrorMessage("'City' must not be empty.");
                validator.ShouldHaveValidationErrorFor(x => x.AddressLine1, patientCommand)
                   .WithErrorMessage("'Address Line1' must not be empty.");
                validator.ShouldHaveValidationErrorFor(x => x.AddressLine2, patientCommand)
                   .WithErrorMessage("'Address Line2' must not be empty.");
                validator.ShouldHaveValidationErrorFor(x => x.PreferredName, patientCommand)
                   .WithErrorMessage("'Preferred Name' must not be empty.");
            }

            [Test]
            public void CreatePatientCommandValidator_MaxStringValues_ValidationSuccessful()
            {
                var patientCommand = fixture
                        .Build<CreatePatientCommand>()
                        .With(x => x.City, string.Join(string.Empty, fixture.CreateMany<char>(24)))
                        .With(x => x.AddressLine1, string.Join(string.Empty, fixture.CreateMany<char>(20)))
                        .With(x => x.AddressLine2, string.Join(string.Empty, fixture.CreateMany<char>(20)))
                        .With(x => x.PreferredName, string.Join(string.Empty, fixture.CreateMany<char>(20)))
                        .Create();

                validator.ShouldNotHaveValidationErrorFor(x => x.City, patientCommand);
                validator.ShouldNotHaveValidationErrorFor(x => x.AddressLine1, patientCommand);
                validator.ShouldNotHaveValidationErrorFor(x => x.AddressLine2, patientCommand);
                validator.ShouldNotHaveValidationErrorFor(x => x.PreferredName, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_MaxStringValuesPlus1_ValidationFails()
            {
                var patientCommand = fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.City, string.Join(string.Empty, fixture.CreateMany<char>(25)))
                    .With(x => x.AddressLine1, string.Join(string.Empty, fixture.CreateMany<char>(22)))
                    .With(x => x.AddressLine2, string.Join(string.Empty, fixture.CreateMany<char>(23)))
                    .With(x => x.PreferredName, string.Join(string.Empty, fixture.CreateMany<char>(21)))
                    .Create();

                validator.ShouldHaveValidationErrorFor(x => x.City, patientCommand);
                validator.ShouldHaveValidationErrorFor(x => x.AddressLine1, patientCommand);
                validator.ShouldHaveValidationErrorFor(x => x.AddressLine2, patientCommand);
                validator.ShouldHaveValidationErrorFor(x => x.PreferredName, patientCommand);
            }
        }
    }
}
