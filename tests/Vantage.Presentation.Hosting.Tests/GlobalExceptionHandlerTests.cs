using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Vantage.Presentation.Hosting.Errors;

namespace Vantage.Presentation.Hosting.Tests
{
    public class GlobalExceptionHandlerTests
    {
        [Fact]
        public async Task TryHandleAsync_ReturnsTrue_AndSetsCorrectStatusCode()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GlobalExceptionHandler>>();
            var handler = new GlobalExceptionHandler(loggerMock.Object);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new Exception("Test error");

            // Act
            var result = await handler.TryHandleAsync(context, exception, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(500, context.Response.StatusCode);
        }

        [Fact]
        public async Task TryHandleAsync_ValidationException_Returns400BadRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GlobalExceptionHandler>>();
            var handler = new GlobalExceptionHandler(loggerMock.Object);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Simulate a FluentValidation failure
            var failures = new List<ValidationFailure>
        {
            new ValidationFailure("EscrowAmount", "Amount must be greater than zero.")
        };
            var exception = new ValidationException(failures);

            // Act
            var result = await handler.TryHandleAsync(context, exception, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(400, context.Response.StatusCode);
        }
    }
}
