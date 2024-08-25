using CleanSheet.Application.Features.Auth.Commands.Login;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanSheet.Presentation.Endpoints.Auth;
public class LoginEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("login", HandleAsync)
            .WithName("Auth: Login")
            .WithSummary("Login user")
            .WithDescription("Authenticate user and return token")
            .WithOrder(1)
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        LoginRequest request,
        ISender sender)
    {
        var result = await sender.Send(request.ToCommand());
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : result.HandleFailure();
    }
}
