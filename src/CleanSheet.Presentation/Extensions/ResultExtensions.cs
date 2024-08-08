using CleanSheet.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanSheet.Presentation.Extensions;
public static class ResultExtensions
{
    public static IResult HandleFailure(this Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult => TypedResults.BadRequest(
                CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error!,
                    validationResult.Errors
                )),
            _ => TypedResults.BadRequest(CreateProblemDetails(
                "Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error!
                ))
        };
    

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
