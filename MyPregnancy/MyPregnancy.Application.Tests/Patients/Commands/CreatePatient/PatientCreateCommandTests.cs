namespace MyPregnancy.Application.Tests.Patients.Commands.CreatePatient
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using MyPregnancy.Domain.Entities;
    using MyPregnancy.Persistence;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class PatientCreateCommandTests
    {
        private MyPregnancyDbContext _myPregnancyDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyPregnancyDbContext>()
            .UseInMemoryDatabase(databaseName: "MyPregnancy")
            .Options;

            _myPregnancyDbContext = new MyPregnancyDbContext(options);
            _myPregnancyDbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task CreatePatientCommand_ValidCreateCommand_SuccessfullyCreatesPatient()
        {
            var mapperMock = Substitute.For<IMapper>();
            var loggerMock = Substitute.For<ILogger<CreatePatientCommand>>();
            CreatePatientCommand testCommand = new CreatePatientCommand { Surname = "Steve" };

            mapperMock.Map<Patient>(Arg.Is<CreatePatientCommand>(x => x.Surname == testCommand.Surname))
                .Returns(new Patient { Surname = "test1" });

            var sut = new CreatePatientCommand.Handler(_myPregnancyDbContext, mapperMock, loggerMock);
            var result = await sut.Handle(testCommand, CancellationToken.None);

            mapperMock.Received(1).Map<Patient>(Arg.Any<CreatePatientCommand>());
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void CreatePatientCommand_MapperThrowsException_PatientIsNotCreated()
        {
            var mapperMock = Substitute.For<IMapper>();
            var loggerMock = Substitute.For<ILogger<CreatePatientCommand>>();
            CreatePatientCommand testCommand = new CreatePatientCommand { Surname = "Steve" };
            mapperMock.Map<Patient>(
                Arg.Any<CreatePatientCommand>()).Returns(x => { throw new Exception(); });

            var sut = new CreatePatientCommand.Handler(_myPregnancyDbContext, mapperMock, loggerMock);

            Assert.ThrowsAsync<Exception>(async () => await sut.Handle(testCommand, CancellationToken.None));
            Assert.That(_myPregnancyDbContext.Patient.ToList(), Is.Empty);
        }
    }
}
