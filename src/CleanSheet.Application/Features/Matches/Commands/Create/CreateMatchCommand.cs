using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Enums;
using MediatR;

namespace CleanSheet.Application.Features.Matches.Commands.Create;
public record CreateMatchCommand(
    long TeamId,
    long OpponentId,
    MatchLocation Location,
    DateOnly Date,
    Competition Competition,
    string? Stadium) : IRequest<Result<long>>;
