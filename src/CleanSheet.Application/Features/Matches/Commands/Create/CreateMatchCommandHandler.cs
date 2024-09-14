using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Matches.Commands.Create;
public class CreateMatchCommandHandler(
    IMatchRepository matchRepository,
    ITeamRepository teamRepository,
    IOpponentRepository opponentRepository) : IRequestHandler<CreateMatchCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.TeamId, cancellationToken);

        if (team is null)
            return Result.Failure<long>(TeamErrors.NotFound);

        var opponent = await opponentRepository.GetByIdAsync(request.OpponentId, cancellationToken);

        if (opponent is null)
            return Result.Failure<long>(OpponentErrors.NotFound);

        var match = new Match(opponent, request.Location, request.Date, request.Competition);

        var determineStadiumResult = match.DetermineStadium(team, request.Stadium);

        if (determineStadiumResult.IsFailure)
            return Result.Failure<long>(determineStadiumResult.Error);

        team.AddMatch(match);

        await matchRepository.AddAsync(match, cancellationToken);

        return match.Id;
    }
}
