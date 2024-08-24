using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanSheet.Presentation.Endpoints.Careers;
internal class CreateCareerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Career: Create")
            .WithSummary("Create a new career")
            .WithDescription("Create a new career linked to an existing user")
            .WithOrder(1)
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        CreateCareerRequest request)
    {
        var result = await sender.Send(request.ToCommand());
        return result.IsSuccess
            ? TypedResults.Created($"api/v1/careers/{result.Value}", result)
            : result.HandleFailure();
    }
}
