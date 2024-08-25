using CleanSheet.Application.Abstractions;
using CleanSheet.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanSheet.Infrastructure.Authentication;
public sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string Generate(User user)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Username),
        };

        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: signinCredentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}
