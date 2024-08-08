using Microsoft.AspNetCore.Routing;

namespace CleanSheet.Presentation.Abstractions;
public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}
