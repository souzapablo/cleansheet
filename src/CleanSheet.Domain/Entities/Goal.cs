using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Goal : Entity
{
    protected Goal() { }

    public long MatchId { get; private set; }
    public Match Match { get; private set; } = null!;
    public long? PlayerScoredId { get; private set; }
    public Player? PlayerScored { get; private set; } = null!;
    public long? AssistId { get; private set; }
    public Assist? Assist { get; set; }
    public bool IsOwnGoal { get; private set; }
}
