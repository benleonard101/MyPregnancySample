namespace MyPregnancy.WebApi.Tests.Controllers
{
    using AutoFixture;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Application.Patients.Commands.CreatePatient;
    using MyPregnancy.Application.Patients.Queries.GetAllPatients;
    using MyPregnancy.Application.Patients.Queries.GetPatient;
    using MyPregnancy.Common.Dtos;
    using MyPregnancy.WebApi.Controllers;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyPregnancy.Application.Exceptions;

    [TestFixture]
    public class PatientsControllerTests
    {
        [Test]
        public async Task PatientsControllerCreate_ValidCreatePatientCommand_CommandSentSuccessfully()
        {
            int successfulCreationId = 1;
            var mediatorMock = Substitute.For<IMediator>();
            var loggerMock = Substitute.For<ILogger<PatientsController>>();
            mediatorMock.Send(Arg.Any<CreatePatientCommand>()).Returns(successfulCreationId);
            CreatePatientCommand testCommand = new CreatePatientCommand { Surname = "Steve" };
            var sut = new PatientsController(mediatorMock, loggerMock);

            var result = await sut.Create(testCommand);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(successfulCreationId));
            await mediatorMock.Received(1).Send(Arg.Is<CreatePatientCommand>(x => x.Surname == testCommand.Surname));
            loggerMock.Received(1);
        }

        [Test]
        public async Task PatientsControllerGet_ValidGetAllPatientsQuery_GetsAllPatients()
        {
            var mediatorMock = Substitute.For<IMediator>();
            var loggerMock = Substitute.For<ILogger<PatientsController>>();
            var patients = new Fixture().CreateMany<PatientDto>(3).ToList();
            mediatorMock.Send(Arg.Any<GetAllPatientsQuery>()).Returns(patients);
            GetAllPatientsQuery getAllPatientsQuery = new GetAllPatientsQuery();
            var sut = new PatientsController(mediatorMock, loggerMock);

            var result = await sut.Get(getAllPatientsQuery);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That((okResult.Value as List<PatientDto>), Is.EquivalentTo(patients));
            await mediatorMock.Received(1).Send(Arg.Any<GetAllPatientsQuery>());
            loggerMock.Received(1);
        }

        [Test]
        public async Task PatientsControllerGet_ValidGetPatientQuery_GetsPatient()
        {
            int patientId = 1;
            var mediatorMock = Substitute.For<IMediator>();
            var loggerMock = Substitute.For<ILogger<PatientsController>>();
            var patient = new Fixture().Create<PatientDto>();
            mediatorMock.Send(Arg.Any<GetPatientQuery>()).Returns(patient);
            var sut = new PatientsController(mediatorMock, loggerMock);

            var result = await sut.Get(patientId);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That((okResult.Value as PatientDto), Is.EqualTo(patient));
            await mediatorMock.Received(1).Send(Arg.Is<GetPatientQuery>( x => x.PatientId == patientId));
            loggerMock.Received(1);
        }

        [Test]
        public void PatientsControllerGet_InvalidPatientId_ThrowsValidationException()
        {
            int patientId = 0;
            var mediatorMock = Substitute.For<IMediator>();
            var loggerMock = Substitute.For<ILogger<PatientsController>>();
            var patient = new Fixture().Create<PatientDto>();
            mediatorMock.Send(Arg.Any<GetPatientQuery>()).Returns(patient);
            var sut = new PatientsController(mediatorMock, loggerMock);

            Assert.ThrowsAsync<ValidationException>(async () => await sut.Get(patientId));
            loggerMock.Received(1);
        }
    }
}