using AutoFixture;
using MyPregnancy.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyPregnancy.Persistence
{
    using System;

    public class MyPregnancyInitializer
    {
        private readonly Dictionary<int, Patient> Patients = new Dictionary<int, Patient>();

        public static void Initialize(MyPregnancyDbContext context)
        {
            var initializer = new MyPregnancyInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(MyPregnancyDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Patient.Any())
            {
                return; // Db has been seeded
            }

            SeedPatients(context);
        }

        public void SeedPatients(MyPregnancyDbContext context)
        {
            //    List<Patient> patients = new List<Patient>();
            //    Fixture fixture = new Fixture();

            //    for (var i = 1; i < 50; i++){

            //        fixture.Customizations.Add(
            //            new StringGenerator(() =>
            //                Guid.NewGuid().ToString().Substring(0, 3)));
            //        // var patient = fixture.Create<Patient>();

            //      var patient = fixture.Build<Patient>()
            //            .With(x => x.Day,  "1")
            //            .With(x => x.Month, "2")
            //            .With(x => x.Year, "2000")
            //            .With(x => x.EmailAddress, "david@gmail.com")
            //            .With(x => x.Postcode, "BT42 6HY")
            //            .With(x => x.Language, "English")
            //            .Create();


            //        patient.PatientId = i;
            //        patients.Add(patient);
            //    }

            //    context.Patient.AddRange(patients);

            //    context.SaveChanges();
            //}
        }
    }
}