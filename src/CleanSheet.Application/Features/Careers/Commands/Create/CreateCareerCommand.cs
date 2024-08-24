using CleanSheet.Domain.Abstractions;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;
public record CreateCareerCommand(
    long UserId,
    string ManagerFirstName,
    string ManagerLastName) : IRequest<Result<long>>;