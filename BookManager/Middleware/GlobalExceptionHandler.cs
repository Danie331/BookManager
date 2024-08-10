using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BookManager.Middleware
{
    public sealed class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var responseMessage = new StringBuilder();
            if (exception is ValidationException ve && ve.Errors.Any())
            {
                foreach (var error in ve.Errors)
                {
                    responseMessage.AppendLine($"Code: {error.ErrorCode} Message: {error.ErrorMessage}");
                }

                // Arbitrarily use the first error code as the http response given no order of precedence specified
                httpContext.Response.StatusCode = Convert.ToInt32(ve.Errors.First().ErrorCode);
            }
            else
            {
                // Unknown error
                responseMessage.Append(exception.Message);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            var errorDetails = new ProblemDetails { Status = httpContext.Response.StatusCode, Title = responseMessage.ToString() };
            await httpContext.Response.WriteAsJsonAsync(errorDetails, cancellationToken);

            return true; // For purposes of this assignment, don't leave exception unhandled
        }
    }
}
