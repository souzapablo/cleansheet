using CleanSheet.Domain.Enums;

namespace CleanSheet.Application.Features.Matches.Commands.Create;
public record CreateMatchRequest(
    long TeamId,
    long OpponentId,
    MatchLocation Location,
    DateOnly Date,
    Competition Competition,
    string? Stadium)
{
    public CreateMatchCommand ToCommand() =>
        new(TeamId, OpponentId, Location, Date, Competition, Stadium);
};
