namespace MyPregnancy.WebApi.Tests.Middleware
{
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using MyPregnancy.Application.Exceptions;
    using MyPregnancy.WebApi.Middleware;
    using Newtonsoft.Json;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    [TestFixture]
    public class ErrorHandlingMiddlewareTests
    {
        [Test]
        public async Task ErrorHandlingMiddleware_ValidationExceptionRaised_ReturnsBadRquestWithValidationErrors()
        {
            var mockLogger = Substitute.For<ILoggerFactory>();

            // Arrange
            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                throw new ValidationException(
                    new List<ValidationFailure> { new ValidationFailure("Error", "Big error") });
            }
            , mockLogger);


            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            //Act
            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Dictionary<string, string[]> objResponse;

            using (var reader = new StreamReader(context.Response.Body))
            {
                var streamText = reader.ReadToEnd();
                objResponse = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(streamText);
            }

            //Assert
            var firstError = objResponse.TryGetValue("Error", out string[] errors);
            Assert.That(errors[0], Is.EqualTo("Big error"));
            Assert.That(context.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task ErrorHandlingMiddleware_NotFoundExceptionRaised_ReturnsNotFoundtWithId()
        {
            var mockLogger = Substitute.For<ILoggerFactory>();

            // Arrange
            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                throw new NotFoundException("Patient", 1);
            }
            , mockLogger);


            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            //Act
            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            string objResponse;

            using (var reader = new StreamReader(context.Response.Body))
            {
                objResponse = reader.ReadToEnd();
            }

            //Assert
            Assert.That(objResponse, Does.Contain("(1) was not found."));
            Assert.That(context.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        [Test]
        public async Task ErrorHandlingMiddleware_ExceptionRaised_ReturnsInternalServerError()
        {
            var mockLogger = Substitute.For<ILoggerFactory>();

            // Arrange
            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                throw new Exception("Something went wrong");
            }
            , mockLogger);


            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            //Act
            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            string objResponse;

            using (var reader = new StreamReader(context.Response.Body))
            {
                objResponse = reader.ReadToEnd();
            }

            //Assert
            Assert.That(objResponse, Does.Contain("Something went wrong"));
            Assert.That(context.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }
    }
}
