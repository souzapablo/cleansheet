using CleanSheet.Application.Features.Careers.Queries.List;
using CleanSheet.Application.Features.Careers.Responses;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace CleanSheet.Presentation.Endpoints.Careers;

internal class ListUserCareersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("", HandleAsync)
            .WithName("Career: List")
            .WithSummary("List careers")
            .WithDescription("List all user careers")
            .WithOrder(2)
            .Produces<Result<List<CareerResponse>>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        ClaimsPrincipal claims)
    {
        long.TryParse(claims.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value, out var userId);

        var query = new ListCareersQuery(userId);
        var result = await sender.Send(query);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : result.HandleFailure();
    }
}
