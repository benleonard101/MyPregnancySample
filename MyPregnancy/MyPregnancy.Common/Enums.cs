namespace MyPregnancy.Common
{
    using System.ComponentModel.DataAnnotations;

    public static class Enums
    {
        public enum BloodGroup
        {
            [Display(Name = "A+")]
            APositive = 1,
            [Display(Name = "AB+")]
            ABPositive,
            [Display(Name = "B+")]
            BPositive,
            [Display(Name = "O+")]
            OPositive,
            [Display(Name = "O-")]
            ONegative,
        }

        public enum Calculator
        {
            SelfEmployed = 1,
            Employed = 2,
        }
    }
}
