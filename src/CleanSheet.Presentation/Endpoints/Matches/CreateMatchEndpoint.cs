using CleanSheet.Application.Features.Matches.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanSheet.Presentation.Endpoints.Matches;
internal class CreateMatchEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Match: Create")
            .WithSummary("Create a new match")
            .WithDescription("Create a new match linked to an existing team")
            .WithOrder(1)
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        CreateMatchRequest request)
    {
        var result = await sender.Send(request.ToCommand());
        return result.IsSuccess
            ? TypedResults.Created($"api/v1/matches/{result.Value}", result)
            : result.HandleFailure();
    }
}
