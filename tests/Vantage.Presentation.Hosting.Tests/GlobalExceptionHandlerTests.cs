using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Vantage.Presentation.Hosting.Errors;
using Vantage.Presentation.Hosting.Localization;
using Xunit;

namespace Vantage.Presentation.Hosting.Tests
{
    /// <summary>
    /// Contains comprehensive unit tests for the GlobalExceptionHandler, verifying RFC 9457 compliance across various exception states.
    /// </summary>
    public class GlobalExceptionHandlerTests
    {
        private readonly GlobalExceptionHandler _handler;

        #region Fake Localizer for Testing
        /// <summary>
        /// A fake localizer that returns the exact key requested. 
        /// This allows us to prove the localization pipeline is invoked without requiring physical .resx files in the test suite.
        /// </summary>
        private class FakeStringLocalizer : IStringLocalizer<SharedResource>
        {
            public LocalizedString this[string name] => new LocalizedString(name, name);
            public LocalizedString this[string name, params object[] arguments] => new LocalizedString(name, name);
            public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => Enumerable.Empty<LocalizedString>();
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandlerTests"/> class, establishing the testing harness.
        /// </summary>
        public GlobalExceptionHandlerTests()
        {
            var logger = NullLogger<GlobalExceptionHandler>.Instance;
            var localizer = new FakeStringLocalizer();
            _handler = new GlobalExceptionHandler(logger, localizer);
        }

        #region Edge Case Tests

        /// <summary>
        /// Verifies that an UnauthorizedAccessException correctly mutates the HTTP response into a 401 Unauthorized ProblemDetails object.
        /// </summary>
        [Fact]
        public async Task TryHandleAsync_GivenUnauthorizedAccessException_Returns401ProblemDetails()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new UnauthorizedAccessException("Simulated unauthorized attempt.");

            var result = await _handler.TryHandleAsync(context, exception, CancellationToken.None);

            Assert.True(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseJson = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(problemDetails);
            Assert.Equal(StatusCodes.Status401Unauthorized, problemDetails.Status);
            // Assert against the KEY to prove the localizer was called
            Assert.Equal("UnauthorizedTitle", problemDetails.Title);
            Assert.Equal("UnauthorizedDetail", problemDetails.Detail);
        }

        /// <summary>
        /// Verifies that a FluentValidation ValidationException correctly mutates the HTTP response into a 400 Bad Request ProblemDetails object, fully extracting the property error dictionary.
        /// </summary>
        [Fact]
        public async Task TryHandleAsync_GivenValidationException_Returns400ProblemDetailsWithErrors()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Username", "Username must be at least 5 characters."),
                new ValidationFailure("Password", "Password requires a special character.")
            };
            var exception = new ValidationException("Validation failed", validationFailures);

            var result = await _handler.TryHandleAsync(context, exception, CancellationToken.None);

            Assert.True(result);
            Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseJson = await new StreamReader(context.Response.Body).ReadToEndAsync();

            using var document = JsonDocument.Parse(responseJson);
            var root = document.RootElement;

            Assert.Equal(StatusCodes.Status400BadRequest, root.GetProperty("status").GetInt32());
            // Assert against the KEY to prove the localizer was called
            Assert.Equal("ValidationFailedTitle", root.GetProperty("title").GetString());



            var errors = root.GetProperty("errors");

            Assert.True(errors.TryGetProperty("Username", out var usernameErrors));
            Assert.Equal("Username must be at least 5 characters.", usernameErrors[0].GetString());

            Assert.True(errors.TryGetProperty("Password", out var passwordErrors));
            Assert.Equal("Password requires a special character.", passwordErrors[0].GetString());
        }

        /// <summary>
        /// Verifies that an unhandled generic Exception defaults safely to a 500 Internal Server Error without leaking sensitive stack traces to the client.
        /// </summary>
        [Fact]
        public async Task TryHandleAsync_GivenGenericException_Returns500ProblemDetails()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new Exception("A deep database failure occurred that the client must not see.");

            var result = await _handler.TryHandleAsync(context, exception, CancellationToken.None);

            Assert.True(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseJson = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(problemDetails);
            Assert.Equal(StatusCodes.Status500InternalServerError, problemDetails.Status);
            // Assert against the KEY to prove the localizer was called
            Assert.Equal("ServerErrorTitle", problemDetails.Title);
            Assert.Equal("ServerErrorDetail", problemDetails.Detail);

            Assert.DoesNotContain("database failure", responseJson);
        }

        #endregion
    }
}