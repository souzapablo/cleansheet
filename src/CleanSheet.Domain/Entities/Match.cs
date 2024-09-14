using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Enums;
using CleanSheet.Domain.Errors;

namespace CleanSheet.Domain.Entities;
public class Match : Entity
{
    private readonly List<Goal> _goals = [];
    private readonly List<Player> _lineup = [];

    protected Match() { }

    public Match(Opponent opponent, MatchLocation location, DateOnly date,
        Competition competition)
    {
        Opponent = opponent;
        Location = location;
        Date = date;
        Competition = competition;
    }

    public long TeamId { get; private set; }
    public Team Team { get; private set; } = null!;
    public long OpponentId { get; private set; }
    public Opponent Opponent { get; private set; } = null!;
    public MatchLocation Location { get; private set; }
    public DateOnly Date { get; private set; }
    public IReadOnlyCollection<Goal> Goals => _goals;
    public IReadOnlyCollection<Player> Lineup => _lineup;
    public int HomeGoals { get; private set; }
    public int AwayGoals { get; private set; }
    public MatchResult Result { get; private set; }
    public Competition Competition { get; private set; }
    public string Stadium { get; private set; } = string.Empty;

    public Result DetermineStadium(Team team, string? stadium = null)
    {
        if (string.IsNullOrWhiteSpace(stadium) && Location.Equals(MatchLocation.Neutral))
            return Abstractions.Result.Failure(MatchErrors.UndefinedNeutralStadium);

        Stadium = Location switch
        {
            MatchLocation.Home => team.Stadium,
            MatchLocation.Away => Opponent.Stadium,
            MatchLocation.Neutral => stadium ?? string.Empty,
            _ => throw new ArgumentOutOfRangeException()
        };

        return Abstractions.Result.Success();
    }
}
