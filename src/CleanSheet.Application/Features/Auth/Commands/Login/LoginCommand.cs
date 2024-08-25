using CleanSheet.Domain.Abstractions;
using MediatR;

namespace CleanSheet.Application.Features.Auth.Commands.Login;
public record LoginCommand(
    string Username,
    string Password) : IRequest<Result<LoginResponse>>;