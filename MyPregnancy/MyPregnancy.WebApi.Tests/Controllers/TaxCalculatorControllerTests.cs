namespace MyPregnancy.WebApi.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Common;
    using MyPregnancy.Common.Dtos.Calculator;
    using MyPregnancy.TaxCalculators.Interfaces;
    using MyPregnancy.TaxCalculators.TaxCalculators;
    using MyPregnancy.WebApi.Controllers;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class TaxCalculatorControllerTests
    {
        [Test]
        public void TaxCalculatorController_EmployedCalculatorEnum_ReturnsOkResponse()
        {
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var employedLoggerMock = Substitute.For<ILogger<EmployedTaxCalculator>>();
            var factoryMock = Substitute.For<ITaxCalculatorFactory>();
            factoryMock.CreateTaxCalculator(Arg.Is<Enums.Calculator>(x => x == Enums.Calculator.Employed))
                .Returns( new EmployedTaxCalculator(employedLoggerMock));
            var sut = new TaxCalculatorController(loggerMock, factoryMock);

            var result = sut.GetTaxCalculation(new TaxCalculationDto { CalculationType = Enums.Calculator.Employed });

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var responseString = (okResult.Value as TaxCalculationSummary).CalculatorName;
            Assert.That(responseString, Is.EqualTo(nameof(EmployedTaxCalculator)));
            loggerMock.Received(1);
        }

        [Test]
        public void TaxCalculatorController_SelfEmployedCalculatorEnum_ReturnsOkResponse()
        {
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var selfEmployedLoggerMock = Substitute.For<ILogger<SelfEmployedTaxCalculator>>();
            var factoryMock = Substitute.For<ITaxCalculatorFactory>();
            factoryMock.CreateTaxCalculator(Arg.Is<Enums.Calculator>(x => x == Enums.Calculator.SelfEmployed))
                .Returns(new SelfEmployedTaxCalculator(selfEmployedLoggerMock));
            var sut = new TaxCalculatorController(loggerMock, factoryMock);

            var result = sut.GetTaxCalculation(new TaxCalculationDto { CalculationType = Enums.Calculator.SelfEmployed });

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var responseString = (okResult.Value as TaxCalculationSummary).CalculatorName;
            Assert.That(responseString, Is.EqualTo(nameof(SelfEmployedTaxCalculator)));
            loggerMock.Received(1);
        }
    }
}