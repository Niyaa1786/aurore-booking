using Aurore.Application.Common.Exceptions;
using Aurore.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Aurore.Api.Responses;
using System.Net;
using System.Text.Json;

namespace Aurore.Api.Handler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";
            object? errors = null;

            if (exception is FluentValidation.ValidationException valEx)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Validation failed.";
                errors = valEx.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(g => JsonNamingPolicy.CamelCase.ConvertName(g.Key), g => g.Select(e => e.ErrorMessage).ToArray());
            }
            else if (exception is DomainException domainEx)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = domainEx.Message;
            }
            else if (exception is NotFoundException notFoundEx)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = notFoundEx.Message;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "Unauthorized access";
            }
            else if (exception is DbUpdateConcurrencyException)
            {
                statusCode = (int)HttpStatusCode.Conflict;
                message = "An unexpected error occurred. Please try again later.";
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred. Please try again later";
            }

            var response = ApiResponse<object>.Failure(errors!, message);

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
