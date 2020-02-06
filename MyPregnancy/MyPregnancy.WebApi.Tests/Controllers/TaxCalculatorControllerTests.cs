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
    using System.Threading.Tasks;

    [TestFixture]
    public class TaxCalculatorControllerTests
    {
        [Test]
        public async Task TaxCalculatorController_EmployedCalculatorEnum_ReturnsOkResponse()
        {
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var employedLoggerMock = Substitute.For<ILogger<EmployedTaxCalculator>>();
            var factoryMock = Substitute.For<ITaxCalculatorFactory>();
            var calculatorClientMock = Substitute.For<ICalculatorClient>();

            calculatorClientMock.Add(Arg.Is(1), Arg.Is(9)).Returns(10);
            factoryMock.CreateTaxCalculator(Arg.Is<Enums.Calculator>(x => x == Enums.Calculator.Employed))
                .Returns( new EmployedTaxCalculator(employedLoggerMock, calculatorClientMock));
            var sut = new TaxCalculatorController(loggerMock, factoryMock);

            var result = await sut.GetTaxCalculation(new TaxCalculationDto { CalculationType = Enums.Calculator.Employed });

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var taxCalculationSummary = (okResult.Value as TaxCalculationSummary);
            Assert.That(taxCalculationSummary.CalculatorName, Is.EqualTo(nameof(EmployedTaxCalculator)));
            Assert.That(taxCalculationSummary.TotalTax, Is.EqualTo(10));
            loggerMock.Received(1);
            calculatorClientMock.Received(1);
        }

        [Test]
        public async Task TaxCalculatorController_SelfEmployedCalculatorEnum_ReturnsOkResponse()
        {
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var selfEmployedLoggerMock = Substitute.For<ILogger<SelfEmployedTaxCalculator>>();
            var factoryMock = Substitute.For<ITaxCalculatorFactory>();
            var calculatorClientMock = Substitute.For<ICalculatorClient>();


            calculatorClientMock.Add(Arg.Is(9), Arg.Is(9)).Returns(11);
            factoryMock.CreateTaxCalculator(Arg.Is<Enums.Calculator>(x => x == Enums.Calculator.SelfEmployed))
                .Returns(new SelfEmployedTaxCalculator(selfEmployedLoggerMock, calculatorClientMock));
            var sut = new TaxCalculatorController(loggerMock, factoryMock);

            var result = await sut.GetTaxCalculation(new TaxCalculationDto { CalculationType = Enums.Calculator.SelfEmployed });

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var taxCalculationSummary = (okResult.Value as TaxCalculationSummary);
            Assert.That(taxCalculationSummary.CalculatorName, Is.EqualTo(nameof(SelfEmployedTaxCalculator)));
            Assert.That(taxCalculationSummary.TotalTax, Is.EqualTo(19));
            loggerMock.Received(1);
            calculatorClientMock.Received(1);
        }
    }
}