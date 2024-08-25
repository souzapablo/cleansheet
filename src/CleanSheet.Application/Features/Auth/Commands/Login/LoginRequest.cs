using MediatR;

namespace CleanSheet.Application.Features.Auth.Commands.Login;

public record LoginRequest(
    string Username,
    string Password)
{
    public LoginCommand ToCommand() =>
        new(Username, Password);
}
