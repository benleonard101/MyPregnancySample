namespace MyPregnancy.Common.Dtos
{
    using System;
    using static MyPregnancy.Common.Enums;

    public abstract class BasePatientDto
    {
        public string Surname { get; set; }

        public string Forenames { get; set; }

        public string PreferredName { get; set; }

        public string HealthCareNumber { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public string Rhesus { get; set; }

        public string KnownAllergies { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Language { get; set; }

        public bool InterpreterRequired { get; set; }

        public string MobileTelephone { get; set; }

        public string HomeTelephone { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}