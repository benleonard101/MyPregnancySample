namespace MyPregnancy.Persistence.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MyPregnancy.Domain.Entities;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public sealed class MyPregnancyDbContextTests : IDisposable
    {
        private MyPregnancyDbContext _sut;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyPregnancyDbContext>()
                .UseInMemoryDatabase(databaseName: "MyPregnancy")
                .Options;

            _sut = new MyPregnancyDbContext(options);
            _sut.Database.EnsureCreated();

            Seed(_sut);
        }

        private void Seed(MyPregnancyDbContext pregnancyDbContext)
        {
            var patients = new List<Patient>
            {
                new Patient { PreferredName = "Robert" },
                new Patient { PreferredName = "Bob" },
                new Patient { PreferredName = "Ryan" },
            };

            pregnancyDbContext.Patient.AddRange(patients);
            pregnancyDbContext.SaveChanges();
        }

        [Test]
        public void MyPregnancyDbContext_AddPatientToDB_PatientPersistedSuccessfully()
        {
            Patient patient = new Patient { PatientId = 9, PreferredName = "NewPatient" };

            _sut.Patient.Add(patient);
            _sut.SaveChanges();

            IQueryable<Patient> records = _sut.Patient;

            Assert.That(records.Count(), Is.EqualTo(4));
            Assert.That(records.First(x => x.PatientId == patient.PatientId).PreferredName, Is.EqualTo(patient.PreferredName));
        }

        public void Dispose()
        {
            _sut.Dispose();
        }
    }
}