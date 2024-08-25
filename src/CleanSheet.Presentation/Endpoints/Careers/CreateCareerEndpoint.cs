using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

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
        ClaimsPrincipal claims,
        CreateCareerRequest request)
    {
        long.TryParse(claims.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value, out var userId);

        var result = await sender.Send(request.ToCommand(userId));
        return result.IsSuccess
            ? TypedResults.Created($"api/v1/careers/{result.Value}", result)
            : result.HandleFailure();
    }
}
