using CleanSheet.Domain.Abstractions;
using MediatR;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public record CreateUserCommand(
    string Username,
    string Password) : IRequest<Result<long>>;
