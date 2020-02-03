namespace MyPregnancy.Domain.Entities
{
    using static Common.Enums;

    public class MedicalDetail
    {
        public int MedicalDetailId { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public string Rhesus { get; set; }

        public string KnownAllergies { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
