using CleanSheet.Presentation.Abstractions;
using CleanSheet.Presentation.Endpoints.Auth;
using CleanSheet.Presentation.Endpoints.Careers;
using CleanSheet.Presentation.Endpoints.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CleanSheet.Presentation.Endpoints;
public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
                .MapGroup("");

        endpoints.MapGroup("")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "Hello, world!" });

        endpoints.MapGroup("v1/users")
            .WithTags("Users")
            .MapEndpoint<CreateUserEndpoint>();

        endpoints.MapGroup("v1/careers")
            .WithTags("Careers")
            .RequireAuthorization()
            .MapEndpoint<CreateCareerEndpoint>();

        endpoints.MapGroup("v1/auth")
            .WithTags("Auth")
            .MapEndpoint<LoginEndpoint>();
    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
    where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
