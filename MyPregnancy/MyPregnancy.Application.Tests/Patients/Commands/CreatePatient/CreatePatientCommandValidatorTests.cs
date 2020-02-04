namespace MyPregnancy.Application.Tests.Patients.Commands.CreatePatient
{
    using System;
    using NUnit.Framework;
    using FluentValidation.TestHelper;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using AutoFixture;
    using FluentValidation.Results;
    using MyPregnancy.Common;

    public class CreatePatientCommandValidatorTests
    {
        [TestFixture]
        public class PersonValidatorTester
        {
            private CreatePatientCommandValidator _validator;
            private Fixture _fixture;

            [SetUp]
            public void Setup()
            {
                _validator = new CreatePatientCommandValidator();
                _fixture = new Fixture();
            }

            [Test]
            public void CreatePatientCommandValidator_NullProperties_FailsValidation()
            {
                CreatePatientCommand patientCommand = new CreatePatientCommand();

                _validator.ShouldHaveValidationErrorFor(x => x.PreferredName, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Forenames, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Surname, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Language, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.DateOfBirth, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.MobileTelephone, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.HomeTelephone, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.HealthCareNumber, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.KnownAllergies, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.AddressLine1, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.AddressLine2, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.City, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Postcode, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_MaxStringValues_ValidationSuccessful()
            {
                var patientCommand = _fixture
                        .Build<CreatePatientCommand>()
                        .With(x => x.Forenames, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.Surname, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.PreferredName, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.Language, string.Join(string.Empty, _fixture.CreateMany<char>(40)))
                        .With(x => x.MobileTelephone, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.HomeTelephone, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.HealthCareNumber, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.KnownAllergies, string.Join(string.Empty, _fixture.CreateMany<char>(60)))
                        .With(x => x.AddressLine1, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.AddressLine2, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                        .With(x => x.County, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                        .With(x => x.City, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                        .With(x => x.Postcode, string.Join(string.Empty, _fixture.CreateMany<char>(10)))
                        .With(x => x.EmailAddress, "myemail@gmail.com")
                        .Create();

                var validationErrorsList = _validator.Validate(patientCommand).Errors;

                Assert.That(validationErrorsList, Is.Empty);
            }

            [Test]
            public void CreatePatientCommandValidator_MaxStringValuesPlus1_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.Forenames, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.Surname, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.PreferredName, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.Language, string.Join(string.Empty, _fixture.CreateMany<char>(41)))
                    .With(x => x.HealthCareNumber, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.KnownAllergies, string.Join(string.Empty, _fixture.CreateMany<char>(61)))
                    .With(x => x.AddressLine1, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.AddressLine2, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.County, string.Join(string.Empty, _fixture.CreateMany<char>(25)))
                    .With(x => x.City, string.Join(string.Empty, _fixture.CreateMany<char>(25)))
                    .With(x => x.Postcode, string.Join(string.Empty, _fixture.CreateMany<char>(11)))
                    .With(x => x.EmailAddress, "myemail@gmail.com")
                    .Create();

                _validator.ShouldHaveValidationErrorFor(x => x.PreferredName, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Forenames, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Surname, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Language, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.HealthCareNumber, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.KnownAllergies, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.AddressLine1, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.AddressLine2, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.County, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.City, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Postcode, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_ValidateMaxLengthHomeWhenMobileIsNull_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.Forenames, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Surname, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.PreferredName, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Language, string.Join(string.Empty, _fixture.CreateMany<char>(40)))
                    .With(x => x.MobileTelephone, (string)null)
                    .With(x => x.HomeTelephone, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.HealthCareNumber, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.KnownAllergies, string.Join(string.Empty, _fixture.CreateMany<char>(60)))
                    .With(x => x.AddressLine1, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.AddressLine2, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.County, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.City, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.Postcode, string.Join(string.Empty, _fixture.CreateMany<char>(10)))
                    .With(x => x.EmailAddress, "myemail@gmail.com")
                    .Create();

                _validator.ShouldHaveValidationErrorFor(x => x.HomeTelephone, patientCommand);
                _validator.ShouldNotHaveValidationErrorFor(x => x.MobileTelephone, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_ValidateMaxLengthMobileWhenHomeIsNull_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.Forenames, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Surname, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.PreferredName, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Language, string.Join(string.Empty, _fixture.CreateMany<char>(40)))
                    .With(x => x.MobileTelephone, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.HomeTelephone, (string)null)
                    .With(x => x.HealthCareNumber, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.KnownAllergies, string.Join(string.Empty, _fixture.CreateMany<char>(60)))
                    .With(x => x.AddressLine1, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.AddressLine2, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.County, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.City, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.Postcode, string.Join(string.Empty, _fixture.CreateMany<char>(10)))
                    .With(x => x.EmailAddress, "myemail@gmail.com")
                    .Create();

                _validator.ShouldHaveValidationErrorFor(x => x.MobileTelephone, patientCommand);
                _validator.ShouldNotHaveValidationErrorFor(x => x.HomeTelephone, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_ValidateMaxLengthHomeWhenMobileIsEmpty_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.Forenames, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Surname, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.PreferredName, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Language, string.Join(string.Empty, _fixture.CreateMany<char>(40)))
                    .With(x => x.MobileTelephone, string.Empty)
                    .With(x => x.HomeTelephone, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.HealthCareNumber, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.KnownAllergies, string.Join(string.Empty, _fixture.CreateMany<char>(60)))
                    .With(x => x.AddressLine1, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.AddressLine2, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.County, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.City, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.Postcode, string.Join(string.Empty, _fixture.CreateMany<char>(10)))
                    .With(x => x.EmailAddress, "myemail@gmail.com")
                    .Create();

                _validator.ShouldHaveValidationErrorFor(x => x.HomeTelephone, patientCommand);
                _validator.ShouldNotHaveValidationErrorFor(x => x.MobileTelephone, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_ValidateMaxLengthMobileWhenHomeIsEmpty_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.Forenames, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Surname, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.PreferredName, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.Language, string.Join(string.Empty, _fixture.CreateMany<char>(40)))
                    .With(x => x.MobileTelephone, string.Join(string.Empty, _fixture.CreateMany<char>(21)))
                    .With(x => x.HomeTelephone, string.Empty)
                    .With(x => x.HealthCareNumber, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.KnownAllergies, string.Join(string.Empty, _fixture.CreateMany<char>(60)))
                    .With(x => x.AddressLine1, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.AddressLine2, string.Join(string.Empty, _fixture.CreateMany<char>(20)))
                    .With(x => x.County, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.City, string.Join(string.Empty, _fixture.CreateMany<char>(24)))
                    .With(x => x.Postcode, string.Join(string.Empty, _fixture.CreateMany<char>(10)))
                    .With(x => x.EmailAddress, "myemail@gmail.com")
                    .Create();

                _validator.ShouldHaveValidationErrorFor(x => x.MobileTelephone, patientCommand);
                _validator.ShouldNotHaveValidationErrorFor(x => x.HomeTelephone, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_InvalidBloodGroup_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .Create();

                patientCommand.BloodGroup = (Enums.BloodGroup)99;

                _validator.ShouldHaveValidationErrorFor(x => x.BloodGroup, patientCommand);
            }

            [Test]
            public void CreatePatientCommandValidator_EmptyStrings_ValidationFails()
            {
                var patientCommand = _fixture
                    .Build<CreatePatientCommand>()
                    .With(x => x.Forenames, string.Empty)
                    .With(x => x.Surname, string.Empty)
                    .With(x => x.PreferredName, string.Empty)
                    .With(x => x.Language, string.Empty)
                    .With(x => x.HealthCareNumber, string.Empty)
                    .With(x => x.KnownAllergies, string.Empty)
                    .With(x => x.AddressLine1, string.Empty)
                    .With(x => x.AddressLine2, string.Empty)
                    .With(x => x.County, string.Empty)
                    .With(x => x.City, string.Empty)
                    .With(x => x.Postcode, string.Empty)
                    .With(x => x.EmailAddress, "myemail@gmail.com")
                    .Create();

                _validator.ShouldHaveValidationErrorFor(x => x.PreferredName, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Forenames, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Surname, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Language, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.HealthCareNumber, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.KnownAllergies, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.AddressLine1, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.AddressLine2, patientCommand);
                _validator.ShouldNotHaveValidationErrorFor(x => x.County, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.City, patientCommand);
                _validator.ShouldHaveValidationErrorFor(x => x.Postcode, patientCommand);
            }
        }
    }
}
