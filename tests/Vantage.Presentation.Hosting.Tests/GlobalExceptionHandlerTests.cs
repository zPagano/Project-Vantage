using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Vantage.Presentation.Hosting.Errors;
using Xunit;

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
    }
}
