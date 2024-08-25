using CleanSheet.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace CleanSheet.Api.OptionsSetup;

public class JwtOptionsSetup(
    IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string Section = "Jwt";
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(Section)
            .Bind(options);
    }
}
