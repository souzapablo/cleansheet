using CleanSheet.Application.Features.Users.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanSheet.Presentation.Endpoints.Users;
public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("User: Create")
            .WithSummary("Create a new user")
            .WithDescription("Create a new user")
            .WithOrder(1)
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        CreateUserRequest request)
    {
        var result = await sender.Send(request.ToCommand());
        return result.IsSuccess 
            ? TypedResults.Created($"api/v1/users/{result.Value}", result)
            : result.HandleFailure();
    }
}
